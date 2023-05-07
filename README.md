# EventHubsQS

Schema for send/receive

Config:
- Event Hub details
  - Name
  - Namespace (cluster)
  - DNS suffix
  - Connection string
- Storage account details
  - Name
  - Container name
  - Connection string
- Key vault
  - Name
  - DNS suffix

Event Sender:
- Load config
- Initialize KV client
- Initialize Event Producer client
  - Check for input desired authentication method, and load client accordingly
  - If authentication method is connection string, fetch the string from KV
- Initialize a batch container
- Add events
- Send the batch via producer client
- Dispose of the client

Event Receiver:
- Load config
- Initialize KV client
- Initialize Event Receiver client
  - Check for input desired authentication method, and load client accordingly
  - If authentication method is connection string, fetch the string from KV
