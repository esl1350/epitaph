import tkinter as tk

# Define a function to handle the "Run" button click event
def run_simulation():
    robot_index = robot_slider.get()
    obstacle_index = obstacle_slider.get()
    goal_index = goal_slider.get()

    # Replace this with your actual simulation logic
    # You can use the selected indices to determine the robot, obstacle, and goal
    print(f"Robot: {robot_index}, Obstacle: {obstacle_index}, Goal: {goal_index}")
    # Simulate the scenario here

# Create the main window
root = tk.Tk()
root.title("Robot Simulation")

# Set the background color (RGB: 0, 48, 87)
root.configure(bg="#003057")

# Define custom colors for labels and text
label_color = "#b3a369"  # RGB: 179, 163, 105
text_color = "#b3a369"   # RGB: 179, 163, 105

# Create sliders for robot selection, obstacle selection, and goal selection
robot_label = tk.Label(root, text="Robot:", bg="#003057", fg=label_color)
robot_slider = tk.Scale(root, from_=1, to=2, orient="horizontal", bg="#003057", fg=label_color)
obstacle_label = tk.Label(root, text="Obstacle:", bg="#003057", fg=label_color)
obstacle_slider = tk.Scale(root, from_=1, to=3, orient="horizontal", bg="#003057", fg=label_color)
goal_label = tk.Label(root, text="Goal:", bg="#003057", fg=label_color)
goal_slider = tk.Scale(root, from_=1, to=2, orient="horizontal", bg="#003057", fg=label_color)

# Create the "Run" button with a white border
run_button = tk.Button(root, text="Run Simulation", command=run_simulation, bg=text_color, fg=text_color, relief="solid")

# Pack the widgets
robot_label.pack()
robot_slider.pack()
obstacle_label.pack()
obstacle_slider.pack()
goal_label.pack()
goal_slider.pack()
run_button.pack(pady=(10, 0))

# Start the GUI main loop
root.mainloop()
