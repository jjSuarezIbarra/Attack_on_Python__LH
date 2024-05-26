% Read a video file
videoReader = vision.VideoFileReader('test_7.mp4');

% Create video players for visualization
videoPlayer = vision.VideoPlayer('Position', [100 100 680 520]);
maskPlayer  = vision.VideoPlayer('Position', [800 100 680 520]);

% Create a foreground detector
detector = vision.ForegroundDetector('NumGaussians', 3, ...
                                     'NumTrainingFrames', 50, ...
                                     'MinimumBackgroundRatio', 0.7);

% Loop through the video frames
while ~isDone(videoReader)
    % Read a frame from the video file
    frame = step(videoReader);

    % Detect foreground
    mask = step(detector, frame);

    % Get bounding boxes of the detected objects
    stats = regionprops(logical(mask), 'BoundingBox', 'Area');
    boxes = [stats.BoundingBox];

    % Filter out small blobs based on Area
    filteredBoxes = [];
    for i = 1:size(stats, 1)
        if stats(i).Area >= 100
            filteredBoxes = [filteredBoxes; stats(i).BoundingBox]; %#ok<AGROW>
        end
    end
    boxes = filteredBoxes;

    % Estimate the speed of the detected objects
    speeds = estimateSpeed(boxes, frame);

    % Overlay bounding boxes and speeds on the frame
    outFrame = insertObjectAnnotation(frame, 'rectangle', boxes, speeds);

    % Display the current frame and mask
    step(videoPlayer, outFrame);
    step(maskPlayer, mask);
end

% Clean up
release(videoReader);
release(videoPlayer);
release(maskPlayer);

% Helper function to estimate speed
function speeds = estimateSpeed(bboxes, frame)
    speeds = cell(size(bboxes, 1), 1);
    for i = 1:size(bboxes, 1)
        % Assume a fixed width (e.g., 3 meters) for all vehicles
        vehicleWidth = 3;

        % Calculate the apparent width in pixels
        pixelWidth = bboxes(i, 3);

        % Estimate the distance using the camera's focal length
        focalLength = 500; % Example value, adjust as needed
        distance = focalLength * vehicleWidth / pixelWidth;

        % Calculate the speed based on the distance and time
        speed = distance / 1; % Assume a time interval of 1 second

        speeds{i} = sprintf('Speed: %.2f m/s', speed);
    end
end
