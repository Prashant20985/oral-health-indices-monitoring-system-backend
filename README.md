# Oral Health Examination Project Backend

## Overview
The Oral Health Examination project is an ASP.NET application designed for dental practitioners to perform administrative tasks and conduct patient examinations. The system facilitates user management, including the creation, editing, and deletion of users. Additionally, it supports the assessment of patients' oral health, calculating DMFT/DMFS, API Bleeding, BEWE scores, and allows students to conduct examinations on patients to practice and enhance their skills.

## Features:
### User Management
- **Create User**: Admin can add new users, and assign them roles shuch as Admin, Student, Dentist teacher Researcher and Examiner.
- **Edit User**: Modify user details such as name, email, and role.
- **Delete User**: Admin can remove users from the system.
- **Change Activation**: Admin can change the activation status of a user.

### Patient Examinations
- **DMFT/DMFS Calculation**: Conduct oral health examinations to calculate DMFT (Decayed, Missing, Filled Teeth) and DMFS (Decayed, Missing, Filled Surfaces) scores.
- **API Bleeding Assessment**: Evaluate and record API (Approximal Plaque Index) and Bleeding scores for patients.
- **BEWE Evaluation**: Perform the Basic Erosive Wear Examination (BEWE) to assess dental erosive wear.

### Student Examinations
- **Examination Mode**: Students can conduct examinations on patients, calculating scores for various oral health parameters.
- **Student Assessment**: Evaluate student performance and provide feedback based on their examinations.

## Technologies Used
- ASP.NET
- Entity Framework
- PostgreSQL
- C#
