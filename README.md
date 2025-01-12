# CustomCalendar 📅

## Overview
This is a personal project created to solve the problem of having to search for future events in a calendar, for me, having a calendar with only future events and not all days is very useful, and i can plan myself ahead easily. \
CustomCalendar is a robust ASP.NET MVC application designed for efficient event management. It allows users to record, view, and organize events seamlessly. With intuitive CRUD operations for events and categories, advanced search functionality, and specialized features for managing recurring events, CustomCalendar serves as a versatile tool for personal and professional scheduling needs.
## Features ✨

### Event Management
- **Main Page** 🏠: Displays upcoming events with:
  - Event Date.
  - A countdown of days left for each event.
  - A brief description of each event.
- **CRUD Operations** 🏦: Create, Read, Update, and Delete events effortlessly.
- **Search Functionality** 🔎: Search events by:
  - Date
  - Category
  - Importance
- **Recurring Events** 📆: Special functionality to create multiple events:
  - Daily
  - Monthly
  - Yearly

### Previous Events Management
- **Previous Events Page** ⏰: View all past events.
- **Delete All** ❌: A one-click button to clear all previous events.

### Category Management
- **Categories Page**:
  - Perform CRUD operations on categories.
  - Organize events under relevant categories.

### Security 🔒
- **Login System**: Secure access with password protection.
- **Session Timeout**: Automatic logout after 30 minutes of inactivity.
- **Configuration Security**: Password and database connection string stored in `appsettings.json`, which is excluded from the repository.

### Architecture 🏗️
- **MVC Layer**: Contains controllers and views for seamless user interaction.
- **Repository Layer**:
  - Manages database operations and class definitions.
  - Abstracts data access for better maintainability and scalability.

### Database
- Uses **SQL Server** as the database.
- Requires manual setup of the connection string in `appsettings.json`.
- Leverages Entity Framework (EF) for database migrations and table creation.
## API Endpoints 🛠️

CustomCalendar also includes a RESTful API to enable programmatic access to its core functionalities. Below is an overview of the available endpoints:

### Categories 🗂️

- **Create Category:**

  - `POST /api/categories`
  - **Request Body:**
    ```json
    {
      "name": "Category Name"
    }
    ```
  - **Response:** Created category details.

- **Get All Categories:**

  - `GET /api/categories`
  - **Response:**
    ```json
    [
      {
        "id": 1,
        "name": "Category Name"
      }
    ]
    ```

- **Update Category:**

  - `PUT /api/categories/{id}`
  - **Request Body:**
    ```json
    {
      "name": "Updated Category Name"
    }
    ```
  - **Response:** Updated category details.

- **Delete Category:**

  - `DELETE /api/categories/{id}`
  - **Response:** Status message.

### Events 📅

- **Create Event:**

  - `POST /api/events`
  - **Request Body:**
    ```json
    {
      "title": "Event Title",
      "date": "YYYY-MM-DDTHH:MM:SS",
      "eventText": "Details about the event",
      "important": true,
      "categoryId": 1
    }
    ```
  - **Response:** Created event details.

- **Get All Events:**

  - `GET /api/events`
  - **Response:**
    ```json
    [
      {
        "id": 1,
        "title": "Event Title",
        "date": "YYYY-MM-DDTHH:MM:SS",
        "eventText": "Details about the event",
        "important": true,
        "categoryId": 1
      }
    ]
    ```

- **Update Event:**

  - `PUT /api/events/{id}`
  - **Request Body:**
    ```json
    {
      "title": "Updated Event Title",
      "date": "YYYY-MM-DDTHH:MM:SS",
      "eventText": "Updated details about the event",
      "important": false,
      "categoryId": 2
    }
    ```
  - **Response:** Updated event details.

- **Delete Event:**

  - `DELETE /api/events/{id}`
  - **Response:** Status message.

---
## Setup Instructions 🚀

### Prerequisites
1. Install **Visual Studio** with the ASP.NET and web development workload.
2. Ensure **SQL Server** is installed and configured.
3. Install **Entity Framework Core**.

### Steps
1. Clone the repository:
   ```bash
   git clone https://github.com/username/CustomCalendar.git
   ```
2. Navigate to the project directory:
   ```bash
   cd CustomCalendar
   ```
3. Create an `appsettings.json` file in the project root with the following structure:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Your SQL Server connection string here"
     },
     "AppSettings": {
       "Password": "YourSecurePassword"
     }
   }
   ```
4. Use EF Core to update the database:
   ```bash
   dotnet ef database update
   ```
5. Run the application:
   ```bash
   dotnet run
   ```
## Usage 📖
1. **Login**: Use the password defined in `appsettings.json` to access the system.
2. **Create Events**:
   - Navigate to the main page and use the "Add Event" button.
   - Use the "Create Many" feature for recurring events.
3. **Manage Categories**: Go to the Categories page to add or update categories.
4. **View and Delete Previous Events**: Access the Previous Events page for cleanup.
## Technologies Used 🛠️
- **ASP.NET Core MVC**
- **Entity Framework Core**
- **SQL Server**
- **HTML5, CSS3, Bootstrap**
## Future Enhancements 🌟
- **Notification System**: Email or SMS alerts for upcoming events.
- **Advanced Reporting**: Export event data to PDF or Excel.
- **Mobile App Integration**: Sync with a mobile application for on-the-go access.
## Contributing 🤝
Contributions are welcome! Please follow these steps:
1. Fork the repository.
2. Create a new branch:
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. Commit your changes:
   ```bash
   git commit -m "Add your message here"
   ```
4. Push to your branch:
   ```bash
   git push origin feature/your-feature-name
   ```
5. Submit a pull request.

## License 📜
This project is licensed under the MIT License. See the `LICENSE` file for details.


---

Feel free to reach out for questions or suggestions. Happy scheduling! 🎉
