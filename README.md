Here's a README section for your GitHub repository:

---

# Navigating Neurological Realms: VR Diagnosis for Early Parkinson's Detection

## Abstract
Parkinson's disease (PD) is a neurodegenerative disorder that affects millions worldwide, characterized by symptoms such as tremors, bradykinesia, rigidity, and postural instability. Early diagnosis remains a significant challenge due to the subtlety and variability of early-stage symptoms. Leveraging advancements in virtual reality (VR) technology, this project proposes a novel approach for early PD detection. By utilizing VR environments to detect subtle motor and cognitive impairments associated with early-stage Parkinson's, the system aims to improve diagnostic accuracy and enable timely intervention.

## Project Overview
"Navigating Neurological Realms" is a VR-based diagnostic tool designed to detect asymmetry in hand movements during simulated swimming tasks. This tool is built using the Unity game engine and Oculus Quest 2 VR headset, which tracks and records detailed hand movement data for analysis. The ultimate goal is to provide a non-invasive, engaging, and highly sensitive diagnostic tool for early detection of Parkinson's disease.

## Table of Contents
- [Introduction](#introduction)
- [Background and Related Work](#background-and-related-work)
- [Expected Achievements](#expected-achievements)
- [Software Engineering Process](#software-engineering-process)
- [Evaluation Plans](#evaluation-plans)
- [References](#references)

## Introduction
Parkinson’s Disease (PD) is a progressive neurodegenerative disease affecting the brain's substantia nigra, leading to motor symptoms such as tremor, bradykinesia, and postural instability. This project focuses on detecting asymmetry in motor functions as a potential early indicator of PD.

## Background and Related Work
Current detection methods for PD include DaTSCAN and the MDS-UPDRS scale. VR methods are emerging as powerful tools for PD diagnosis. This project builds on existing VR techniques and aims to enhance early detection through a VR swimming simulation test.

## Expected Achievements
1. **Early and Accurate Detection**: Develop a proof of concept for a VR tool to measure asymmetry in hand movements, potentially aiding in early PD diagnosis.
2. **Improved Patient Experience**: Offer a more immersive and user-friendly diagnostic process, reducing anxiety and improving compliance.
3. **Data Collection and Analysis**: Gather high-fidelity movement data to refine machine learning algorithms for detecting asymmetries.
4. **Additional Applications**: Extend the platform to other neurological diagnostics and rehabilitation programs.

## Software Engineering Process
The project involves researching PD, designing the VR environment, and implementing the movement tracking and data analysis systems. Using the Unity engine and Oculus Quest 2, the system tracks hand movements during a simulated swimming task and records the data in CSV files for analysis.

### Product
The VR system tracks the user's movements during the exercise and records the data into a CSV file. The recorded data includes the position, rotation, timing, and stroke events of each hand. This data is analyzed to detect asymmetries, which may indicate early-stage PD.

### CSV File Example
![CSV Example](link-to-csv-example)

### Data Analysis
The analysis involves calculating the Index of Asymmetry (IA) for various parameters and comparing the results to a threshold value to determine the presence of asymmetry.




