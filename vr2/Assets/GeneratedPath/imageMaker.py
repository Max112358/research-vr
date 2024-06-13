import matplotlib.pyplot as plt
from matplotlib.image import imread

def plot_and_save_path(background_image_path, path_file, output_path):
    # Read coordinates from the text file
    with open(path_file, 'r') as file:
        lines = file.readlines()

    x_coords = []
    y_coords = []

    for line in lines:
        x, y = map(float, line.split())
        x_coords.append(x)
        y_coords.append(y)

    # Load the existing PNG image as background
    background_image = imread(background_image_path)

    # Plot the background image
    plt.imshow(background_image)

    # Plot the points with smaller dots
    plt.scatter(x_coords, y_coords, color='red', s=1)  # Adjust the size here

    # Remove axis
    plt.axis('off')

    # Save the plot as a PNG image with white background and higher DPI
    plt.savefig(output_path, bbox_inches='tight', pad_inches=0, transparent=True, dpi=775)  # Adjust DPI here

    # Clear the plot
    plt.clf()

# Call the function for each path
plot_and_save_path('background.png', 'rightPath.txt', 'output_right.png')
plot_and_save_path('background.png', 'leftPath.txt', 'output_left.png')
