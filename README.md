# File Management System - C#, .NET Core, React.js

## Description

This project is a comprehensive File Management System built using C#, .NET Core, and React.js, with a PostgreSQL database. The application allows users to manage their files securely, featuring functionality for uploading, deleting, renaming files, and enforcing file upload limits. The system also includes robust authentication and authorization mechanisms using JWT (JSON Web Tokens), ensuring that only authorized users can access and manipulate their files.

## Features

- **Secure Authentication and Authorization**: Implements JWT for secure user authentication and role-based authorization, ensuring that only authenticated users can access their files.
- **File Management**: Users can upload, delete, and rename files within the system.
- **File Upload Limits**: Each user has a specific upload limit, with the system keeping track of the used storage space.
- **Responsive Frontend**: A user-friendly interface built with React.js, ensuring smooth and intuitive file management.
- **PostgreSQL Integration**: Uses PostgreSQL for secure and efficient data storage, managing user information, files, and access control.

## Technologies Used

- **C#**: Core programming language for backend development.
- **.NET Core**: Framework used for building the web application’s backend, handling business logic, and API services.
- **React.js**: JavaScript library used for building the responsive and dynamic frontend interface.
- **TypeScript**: Superset of JavaScript used in the frontend to ensure type safety and improved code quality.
- **PostgreSQL**: Database system used to store user data, files, and permissions securely.
- **JWT (JSON Web Tokens)**: Used for secure user authentication and authorization.

## Usage

1. **User Registration and Login**:
   - Users must register and log in to access the file management features.
   - JWT tokens are issued upon successful login, enabling secure access to the system.
2. **File Management**:
   - Upload: Users can upload files until their storage limit is reached.
   - Delete: Files can be deleted from the system by the user.
   - Rename: Users can rename their files for better organization.
3. **Upload Limit**:
   - The system tracks the total storage used by each user and prevents further uploads once the limit is reached.

## Acknowledgments

- Thanks to the open-source communities behind .NET Core, React.js, PostgreSQL, and JWT for providing powerful tools and documentation.
- Special thanks to the contributors who provided libraries and resources that made this project possible.

## Contact

For any questions or further information, please feel free to contact me through my [GitHub profile](https://github.com/MuhammadSohaib-240).
