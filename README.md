# Exercise: Building a Task Management API

## Overview:

You are tasked with creating a basic CRUD API for a task management system using .NET. The API should allow users to perform the following actions:

Create a new task.
  - Retrieve a list of tasks with pagination and filtering.
  - Retrieve a single task by its ID.
  - Create a Task.
  - Update a task.
  - Delete a task.

## Requirements:

1) Create a .NET 6 (or more) Core Web API project that follow basic API design standars. Pay special attention to endpoint naming convention, usual type of server response return (json object in camelCase), ...

2) Define a Task entity with the following properties:

Id (int): Unique identifier for the task.
Title (string): Title of the task.
Description (string): Description of the task.
DueDate (DateTime): Due date of the task.
IsCompleted (bool): Indicates whether the task is completed or not.

3) Implement CRUD operations for tasks using the database and an ORM/SQL builder of your choice.

4) Create a controller with the following API endpoints:

POST: Create a new task.
GET: Retrieve a list of tasks with pagination and filtering (e.g., filter by title, status, or due date).
GET: Retrieve a single task by its ID.
PUT: Update a task by ID.
PATCH: Complete a task. a task can only be completed on thursday
DELETE: Delete a task by ID.

5) Implement validation for input data, such as required fields and proper date formats.

6) Create another entity called Comment with the following properties:

Id (int): Unique identifier for the comment.
Text (string): Text content of the comment.
TaskId (int): Foreign key to associate a comment with a task.

7) Define a relationship between the Task entity and the Comment entity to emulate intermediate entity mapping. A task can have multiple comments, and each comment is associated with a single task.

8) Implement pagination for the list of tasks and comments.