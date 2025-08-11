# Thoughts about the demands of the Ticket System

## The tickets themselves:
- Tickets have a title, problem description, and a status.
- The status can be Open or Closed.

## The requirements of the system:
1. Should be able to create new tickets.
2. Should be able to view all tickets.
3. Should be able to mark a ticket as closed.
4. Should have a nice way to exit the program.
5. There should be a maximum of 5 open tickets at a time.

## Technical requirements:
- Use arrays or lists to store the tasks and their status.
- use methods (functions) to organize the code.
- Use while-loops, for-loops, and/or switch statements/expressions
- Use the console input from the user and show information in the console.
- Show clear messages to the user the whole time.
- use git for source and version control.
- variables, functions, arrays and so on, should be in english.

## Additional challenges:
- Posibility of deleting a ticket.
- Save the tickets to a text file (perhaps JSON is allowed).
- Sort the tickets by status (Open first, then Closed).
- Use Colors.

## Free thoughts:

So although the most standard and widely used way to store the data of these tickets
would be an object. it is stated in the requirements that we should use arrays or lists.
I will go for a hybrid approach, where i will have a ticket object, and have a static list of the tickets (List<Ticket> tickets = new List<Ticket>();)
I will also use a static class to hold the methods that will be used to manipulate the tickets.
This class will be called TicketManager. and include methods like CreateTicket, ViewTickets, CloseTicket, DeleteTicket, SaveTicketsToFile, SortTicketsByStatus
The SaveTicketsToFile method will use JSON to save the tickets to a file. and it will both be a navigational option in the main menu, but also be called when the program exits. (perhaps with a question to the user if they want to save the tickets before exiting))
The SortTicketsByStatus method will use a for-loop to sort the tickets by status, and will be called when the user views the tickets.

The Ticket class will have the following properties:
- Title: string
- Description: string
- IsOpen: bool

 
The Main menu will look like this:

####################
Ticket System Menu
####################
1. Create Ticket
2. View Tickets
3. Close Ticket
4. Delete Ticket
5. Save Tickets to File
6. Exit
####################
Enter your choice: #

The text will be coloured such that the create ticket option is green, the view option is blue, the close option is yellow, the delete option is red, the save option is cyan, and the exit option is magenta.
We need to figure out how we want to handle the user clicking the Create ticket, when there are already 5 open tickets. perhaps the option should be greyed out, or the user should be informed that they cannot create a new ticket until one is closed. both might be good options

The View Tickets screen will look like this:

####################
Tickets Overview
####################
1. {Name} - {Status}
2. {Name} - {Status}
3. {Name} - {Status}
####################
Enter the number of the ticket to view it's details or 0 to go back: #

1. 
The Ticket Details screen will look like this:

####################
Ticket Details
####################
Title: {Title}
Description: {Description}
Status: {Status}
####################
Press any key to go back to the tickets overview...


The Exit should ask the user if they want to save the tickets before exiting:

####################
Exit Ticket System
####################
Do you want to save the tickets before exiting? (y/n): #