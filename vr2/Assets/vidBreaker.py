import cv2
import os

def convert_360_to_panoramic(video_path, output_folder):
    # Create the output folder if it doesn't exist
    if not os.path.exists(output_folder):
        os.makedirs(output_folder)

    # Open the video file
    video = cv2.VideoCapture(video_path)

    # Get the video properties
    width = int(video.get(cv2.CAP_PROP_FRAME_WIDTH))
    height = int(video.get(cv2.CAP_PROP_FRAME_HEIGHT))
    fps = int(video.get(cv2.CAP_PROP_FPS))
    total_frames = int(video.get(cv2.CAP_PROP_FRAME_COUNT))

    # Set the desired output image size
    output_width = width
    output_height = height // 2

    # Initialize the frame counter
    frame_count = 0

    while True:
        # Read a frame from the video
        ret, frame = video.read()

        if not ret:
            break

        # Convert the frame to an equirectangular projection
        equirectangular_frame = cv2.resize(frame, (output_width, output_height))

        # Save the equirectangular frame as an image
        output_path = os.path.join(output_folder, f"panoramic_screenshot_{frame_count:04d}.jpg")
        cv2.imwrite(output_path, equirectangular_frame)

        frame_count += 1

    # Release the video capture object
    video.release()

    print(f"Converted {frame_count} frames to panoramic screenshots.")

# Example usage
video_path = "VID_20240607_103240_00_006.mp4"
output_folder = "panoramic_screenshots"
convert_360_to_panoramic(video_path, output_folder)
