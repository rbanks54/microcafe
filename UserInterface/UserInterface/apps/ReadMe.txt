// ------------------------------------------------
// 
// NOTES:
//
// The 'apps' folder holds the different application
// modules for use in the project.
//
// 0. Shell  - This is the structure of the appliation
// 
// 1. Common - these are the generic services and helpers
//             that are used across the different application
//             spaces.
//
// 2. Journey Designer - these are the angular items for the
//             main focus of this application.
//
// 3. Master Data - these are the angular items for the
//             master reference data shared across different
//             modules.
//
// ------------------------------------------------

Design Interactions:
1. Starting Page - /apps/JourneryDesigner/index.html
2. Api Gateway - configured to use /controllers
3. Token Security - configured in StartUp.cs. 
                  - It interacts with SecurityApi project.

Original Sources:
1. http://www.pluralsight.com/courses/angularjs-playbook
2. http://www.json-generator.com/ - this is for dummy data generation.
