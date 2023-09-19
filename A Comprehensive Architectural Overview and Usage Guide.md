**iLogger System Architectural Overview**

**1. Purpose:**  
The iLogger system provides a configurable and extendable logging mechanism for applications. This allows for sophisticated logging operations such as instance-based targeting, type-based targeting, and value-based criteria for log generation.

**2. Core Components:**

- **iLogger Class:**  
  - Centralized management of logging operations.
  - Handles collection of logger targets (e.g., file, console).
  - Provides methods for logging different severity levels (e.g., Trace, Debug, Info).
  - Offers buffer management (on/off) for logging optimization.

- **ILoggerTarget Interface:**  
  - Provides a consistent logging behavior across different logging destinations.

- **LogFilter Class:**  
  - Processes and filters logs before they are written to their target.
  - Supports inclusion of timestamps, instance names, task IDs, thread IDs, etc.

**3. Features:**

- **Buffered Logging:**  
  - Log entries can be buffered using a Channel.
  - Buffer can be toggled on or off using the `BufferEnabled` property.

- **Instance-based Targeting:**  
  - Allows specific instances of objects to be monitored by the logger.
  - Provides means to name instances for clearer log differentiation.

- **Dynamic Logging Configuration:**  
  - Loggers can be dynamically configured at runtime, affecting which objects and values are logged.
  - Supports custom configuration like enabling/disabling timestamps, thread IDs, etc.

- **Requester Validation:**  
  - Provides a dynamic filtering mechanism based on the properties or type of the loggable object.
  - Supports filtering logs based on numeric value characteristics, object types, and other custom criteria.

**4. Flow of a Log Entry:**

1. A log request is made via methods like `Info`, `Debug`, etc., or by performing operations on the loggable objects.
2. The log entry is processed using `LogFilter` to include/exclude certain data based on configuration.
3. Based on the `BufferEnabled` status and `RequesterValidation`:
   - The log entry may be buffered, dispatched immediately, or discarded based on filtering rules.
4. Log entries from the buffer are periodically dispatched to logger targets.

**5. Extension Points:**

- **New Logger Targets:**  
  - Implement the `ILoggerTarget` interface to introduce new logging destinations (e.g., databases, cloud storage).

- **Custom Log Processing:**  
  - Modify or extend the `LogFilter` class for custom log processing or filtering logic.

- **Future Enhancements (ToDos):**  
  - Context targeting based on namespaces.
  - Loading different configurations from files and applying them dynamically.
  - Support multiple requester filters and acceptance lists.
  - Implement durable and periodic batching mechanisms for efficient log delivery.
  - Allow iLogger to act as a filter layer that can be plugged into popular logging frameworks.
