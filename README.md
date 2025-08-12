# Thoughts about the demands of the Ticket System (Solution Proposal)

## Purpose of the System
The Ticket System is designed to manage simple helpdesk or issue tickets directly from a console interface.

**Core goals:**
- Create, view, close, delete, and save tickets.
- Provide user-friendly console interaction with colored output.
- Limit the number of open tickets to encourage resolution before new issues are created.
- Support persistent storage via JSON files, with the option to load previously saved tickets.

---

## Requirements and How They Are Fulfilled

### Ticket Structure
- **Properties**:
  - `Title` (string)
  - `Description` (string)
  - `IsClosed` (bool – `false` by default)
- Implemented as a C# `record` (`Ticket` class).

### Functional Requirements
1. **Create new tickets**  
   - `CreateTicketScreen()` prompts for title and description.  
   - Creation is blocked if there are already 5 open tickets.

2. **View all tickets**  
   - `ViewTicketsScreen()` lists tickets with alternating colors.  
   - Users can select a ticket to see full details (`ViewTicketDetailsScreen()`).

3. **Mark a ticket as closed**  
   - `CloseTicketScreen()` allows selecting a ticket by number and marking it as closed via `TicketManager.CloseTicket()`.

4. **Delete a ticket**  
   - `DeleteTicketScreen()` removes the ticket from the list.

5. **Save tickets to file**  
   - Saves in JSON format through `TicketManager.SaveTicketsToFileAsync()` and `FileWriter.WriteToJsonAsync()`.

6. **Exit gracefully**  
   - On exit, user is prompted to save tickets before quitting.

7. **Load saved tickets**  
   - At startup, `TicketLoader.GetSaveFiles()` detects `.json` files and allows loading one into the current session.

---

## Technical Requirements
- **Data Storage**:  
  `List<Ticket>` is used to store tickets during runtime.  
  JSON is used for persistent storage.

- **Code Organization**:  
  The system is divided into:  
  - **Core Logic**: `TicketManager`, `Ticket`, `FileReader`, `FileWriter`  
  - **UI Screens**: Separate classes in `TicketSystem.Views` for each menu or action.  
  - **Helpers**: `ConsoleHelpers` for colored text and formatting.

- **Control Flow**:  
  Uses loops (`while(true)`), switch statements, and user input validation.

- **Console Interaction**:  
  Color coding for clarity and user experience.

- **Version Control**:  
  Intended to be managed via Git.

---

## Additional Features Implemented
- **Alternating color rows** in ticket lists for easier reading.
- **Automatic sorting by status** (Open before Closed).
- **Multiple save formats in theory** (TXT examples included in `FileWriter` though not used).
- **Rainbow-colored headers** for main sections.
- **Ticket count limit** to maintain manageable workload.

---

## Menu Structure
```

####################
Main Menu
####################

1. Create Ticket (Green)
2. View Tickets (Blue)
3. Close Ticket (Yellow)
4. Delete Ticket (Red)
5. Save Tickets to File (Cyan)
6. Exit (Magenta)

```

---

## Handling Special Cases
- If user tries to create a ticket when the limit is reached → error message, return to menu.
- Invalid ticket number input → user is warned and prompted again.
- Saving before exit is optional but prompted.
- On startup, the program offers to load previous saves if found.

---

# End User Guide

## Overview
The Ticket System lets you manage a list of issues or support requests from the terminal.  
You can create, view, close, delete, and save tickets — with colorful menus for easier navigation.

---

## Starting the Program
1. Run the program executable.
2. If saved ticket files are found, you will be prompted to load one.
3. You will see the **Main Menu**.

---

## Main Menu Options
1. **Create Ticket**  
   - Enter a title and description for your ticket.  
   - If there are already 5 open tickets, you must close or delete one first.

2. **View Tickets**  
   - Displays a list of tickets with their status (Open or Closed).  
   - Select a ticket number to view its full details.

3. **Close Ticket**  
   - Choose a ticket number to mark as closed.

4. **Delete Ticket**  
   - Permanently remove a ticket from the list.

5. **Save Tickets to File**  
   - Enter a filename or press Enter to use the default (`tickets.json`).  
   - Saves all tickets to disk.

6. **Exit**  
   - You will be asked if you want to save your tickets before quitting.

---

## Tips for Use
- **Keep your ticket list small**: Only 5 open tickets are allowed at once.
- **Save regularly** to avoid losing your work.
- **Closed tickets remain stored** until deleted manually.

## Secret
- **Easter Egg**: Try typing 7 in the main menu.
