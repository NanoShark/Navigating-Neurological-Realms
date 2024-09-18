
# Navigating Neurological Realms: VR Diagnosis for Early Parkinson's Detection

## Overview

"Navigating Neurological Realms" is a virtual reality (VR) diagnostic tool designed to detect early-stage Parkinson's disease by analyzing hand movement asymmetry. This project utilizes the **Oculus Quest 2** and is built using **Unity** and the **Oculus Integration SDK**. Through swimming-like hand movements tracked in real-time, the system gathers and exports movement data to CSV files, enabling detailed analysis of motor symptoms.

## Key Features

- **VR-Based Diagnosis**: Immersive VR environment designed to test hand movement symmetry.
- **Hand Movement Tracking**: Both hand positions and rotations are recorded, as well as the timing and stroke events.
- **Data Analysis**: CSV files generated after each session contain all relevant data for in-depth analysis.
- **Feedback Mechanism**: Haptic, audio, and visual feedback are provided to users during the swimming movement tests.
- **Oculus Quest 2 Integration**: The simulation runs directly on the Quest 2, and data is transferred to a PC for further analysis.
  
## Project Structure

- **Scenes**:
  - **Main Menu**: A simple interface where users can input their names and begin the simulation.
  - **Swimming Simulation**: The core of the application, where users perform hand movements while the system tracks the data.
  
- **Data Collection**:
  - The system records hand positions, rotations, and timings into CSV files for analysis.
  
- **Scripts**:
  - The Unity scripts manage VR interactions, hand tracking, and CSV file generation.
  
- **Analysis**:
  - Python scripts (stored in Google Colab) are used for analyzing the CSV data to detect patterns indicative of Parkinson's disease.

## Installation

### Prerequisites

- **Unity 2021.3.x** (or higher)
- **Oculus Integration SDK** (latest version)
- **Oculus Quest 2** headset
- **Python 3.x** for CSV data analysis

### Setup

1. Clone this repository:
   ```bash
   git clone https://github.com/NanoShark/Navigating-Neurological-Realms.git
   ```
2. Open the project in Unity.
3. Ensure the **Oculus Integration SDK** is installed and properly configured.
4. Build the Unity project and deploy it to your Oculus Quest 2 headset.
5. To analyze data, transfer CSV files from the Quest 2 to your computer and use the provided Python scripts.

## Usage

1. **Start the application**: Enter the main menu in VR, input your name, and begin the swimming simulation.
2. **Perform the test**: Use hand movements to simulate swimming while the system tracks your performance.
3. **End the session**: After the test concludes, CSV data will be generated for further analysis.
4. **Data analysis**: Use the Python script provided to process the CSV data and identify any signs of asymmetry in hand movements.

## Data Analysis

The system outputs CSV files that include:

- **Hand Positions & Rotations**: Both left and right hand data over time.
- **Stroke Events**: Timing and synchronization between hand movements.
  
Python scripts are used to analyze this data to detect hand movement asymmetry, a potential indicator of Parkinson's disease.

## Known Issues

- **Haptic Feedback Intensity**: The current haptic feedback may feel too strong during swimming movements.
- **No UI Assets**: The project currently doesn't use any custom assets for the UI/UX.

## Contributing

Contributions are welcome! Please submit any issues or pull requests, and ensure that your code follows the project's guidelines.

