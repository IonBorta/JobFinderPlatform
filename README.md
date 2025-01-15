App Features:
- Login and Registration
- Company CRUD Operation
- Job CRUD Operation
- Apply for a job, see all job applications
- Create a CV and download it


Design patterns used:
1. Abstract Factory and Bridge: Any time you can switch to another DB. Abstract Factory will provide all repositories based on chosen DB, and its concrete implementation is implemented by bridge pattern.
   ![AbstractFactoryAndBridge](https://github.com/user-attachments/assets/83838e9a-b997-4e85-bb7b-3951de6bf325)
2. Factory Method and Strategy: Factory Method provides the specific strategy for filtering jobs based on their type.
   ![FactoryMethodAndStrategy](https://github.com/user-attachments/assets/792d7549-f7fc-4103-baa0-5b3d7443296d)
3. Builder: You can create a personalized CV where some sections are optional such as: known languages, skills, hobbies, but personal information and work experience are required.
   ![image](https://github.com/user-attachments/assets/0ed3e1be-5fec-4948-9949-799216c4dd3d)
4. Decorator is used for caching jobs.
   ![image](https://github.com/user-attachments/assets/3ff7b0e5-84fa-483a-8697-4ddcc3022480)
5. State is used to manage the behavior of users on job application based on its status. For example user can withdraw and reaply the CV only if the CV wasn't seen yet by the company, or companies can answer only if they saw the CV.
   ![image](https://github.com/user-attachments/assets/6786ad5d-29f1-46b4-93b5-1dde08fff618)
6. Proxy is used for checking user and company access to job applications. For example if user tries to withdraw its own CV, or companies answer only to their job applications.
   ![image](https://github.com/user-attachments/assets/dfb3220c-db69-4b0c-87f4-92de57cee631)
7. Chain of responsibility is used for password validation which requires a certain minimum length, uppercase letters, numbers, and special characters.
   ![image](https://github.com/user-attachments/assets/a22c28de-c55e-47e3-90e5-61abd854cfa3)

Demo of Job Finder Application
https://youtu.be/JJlj-CjOXLQ

