from moviepy.editor import VideoFileClip

# Path to the input MP4 video file
input_video = "VID_20240614_144557_00_029.mp4"

# Load the video file
video = VideoFileClip(input_video)

# Extract the audio from the video
audio = video.audio

# Path to save the output MP3 audio file
output_audio = "audio.mp3"

# Write the audio to the MP3 file
audio.write_audiofile(output_audio)

# Close the video file
video.close()

print("Audio extracted and saved as", output_audio)