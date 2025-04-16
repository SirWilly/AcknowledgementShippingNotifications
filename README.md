# AcknowledgementShippingNotifications

## Current state

### File Watcher
The file watcher is not yet implemented. 
It would be a separate project whose purpose would be to watch a directory and trigger on file drop. In the simplest 
form it would read the file contents as stream and pass to core application for processing. 
Probably in reality we have to account for files being dropped more rapidly than our service can consume them, so
would have to consider a queue. 
Other option would be to periodically read files from the directory one by one. (scheduled task) 
### Core application
The structure of the core application is in place, and it's functionality is proven by unit tests. The structure
of namespaces may change as application evolves.
### DB
The project is implemented, but lacks tests.
I have not implemented a custom batching logic. However, EF does the batching already. Splitting the input as boxes
and adding boxes one by one to DB could be considered as batching. Box and lines are inserted in DB in one round 
trip and EF does batching in max 42 statements.

## Launch instructions

- Initialize the local SQLite DB by executing `dotnet ef database update` command.
- Run the application
