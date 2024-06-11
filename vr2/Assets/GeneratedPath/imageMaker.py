import matplotlib.pyplot as plt
import os
from matplotlib.image import imread

# Read coordinates from the text file
with open('path.txt', 'r') as file:
    lines = file.readlines()

x_coords = []
y_coords = []

for line in lines:
    x, y = map(float, line.split())
    x_coords.append(x)
    y_coords.append(y)

# Load the existing PNG image as background
background_image_path = 'background.png'
background_image = imread(background_image_path)

# Plot the background image
plt.imshow(background_image)

# Plot the points with smaller dots
plt.scatter(x_coords, y_coords, color='red', s=1)  # Adjust the size here

# Remove axis
plt.axis('off')

# Save the plot as a PNG image with white background
output_path = 'output_with_dots.png'
plt.savefig(output_path, bbox_inches='tight', pad_inches=0, transparent=True)

# Display the plot if you want
plt.show()
