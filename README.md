
🎉 Intelligent Logging with iLogger
===================================

Whether you're monitoring a specific class instance or tracking a variable change, iLogger doesn't just observe—it actively assists you in maintaining complete transparency in your application's runtime environment.

This high-performance logging library is built to accommodate various logging scenarios. It's versatile, catering to different needs and customizable, allowing you to focus on specific variables or contexts. This ensures that you can trace your data modifications in real-time, gaining profound insights into your application's flow.

At the heart of iLogger's functionality lies its ability to offer different granularities of log targeting, its flagship feature. You can activate logging exclusively for specific instances, contexts, or namespaces. Furthermore, the logger allows customizing powerful validation functions that direct the logging process, determining what gets logged at runtime.

The name "iLogger" not only symbolizes intelligent logging but also resonates with terms like "interactive", "intuitive", and "innovative". With iLogger, the tasks of debugging and monitoring your application become streamlined and precise, granting you full authority over what, when, and where to log.

Embark on a journey of intelligent, context-aware logging with iLogger. Join the evolution in debugging and monitoring now!

To help keep this project vibrant, active, and free, **💌 [Donate Here](https://www.paypal.com/donate/?hosted_button_id=SJTG7U2E6PC4W) 💌**

💻 Usage
--------



Incorporating iLogger is straightforward. Include it in your class and use the different `Log` levels methods to generate a log entry. Manage contexts and target individual variables with ease. The `Config` object makes adjusting the logger configuration simple.
The `iLogger` library offers a diverse and flexible logging mechanism catering to varied logging needs. This guide showcases some of its prominent features.

### Getting Started

Ensure you have the following namespace imported:

```csharp
using iCode.Log;

```
Start logging:
```csharp
 iLogger.Info("Hello world!!");
```
### Configuration

Initialize and configure the logger:

```csharp
Config loggerConfiguration = iLogger.Filters;
loggerConfiguration.IncludeTimestamp = true;
loggerConfiguration.IncludeInstanceName = true;
loggerConfiguration.IncludeTaskId = true;
loggerConfiguration.IncludeThreadId = true;
```



### Instance Targeting

Target specific instances for logging:

```csharp
NumericLoggableObject firstObject = new NumericLoggableObject();
firstObject.WatchByLogger("FirstObject");
firstObject.UpdateAndLogValue(5);

NumericLoggableObject secondObject = new NumericLoggableObject();
secondObject.UpdateAndLogValue(5);
```

```csharp
class NumericLoggableObject
{
    public void UpdateAndLogValue(int newValue)
    {
        this.Info($"New value assigned: {numericValue}");
    }
    ...

}
```

In this example, only `firstObject` will generate a log when updated, because it's being specifically watched by the logger.

### All Instances Targeting

You can also activate logging for all objects without the need for prior declaration:

```csharp
loggerConfiguration.WatchAllObjects();
```

With the above configuration, all updates to `LoggableObject` instances will be logged.

### Conditional Logging based on Object Characteristics

Implement custom logic to decide whether an object should generate logs:

```csharp
loggerConfiguration.RequesterValidation = (x) => 
{
    if (x is NumericLoggableObject loggableObject)
    {
        return loggableObject.IsOdd();
    }
    return true;
};
```

Here, logging will only occur if the `numericValue` inside the `LoggableObject` is odd.

### Type-based Logging

Log based on the type of the object:

```csharp
loggerConfiguration.RequesterValidation = (x) => x is NumericLoggableObject;
```

With this, only instances of `NumericLoggableObject` will generate logs.

With iLogger, the possibilities are limitless. Your logs can now be more insightful and versatile, assisting you in your development process.

🎯 Potential Usages
-------------------

iLogger can be employed in numerous ways to assist in system development and debugging:

* Customized Debugging
* Performance Monitoring
* Security Auditing
* User Behavior Analysis
* Fine-grained Error Tracking
* Educational Purposes
* Development Teams

Each usage of iLogger brings you closer to a more streamlined and efficient development and debugging process.

🔍 Description
--------------


**Buffer Enabled Mode**

When the BufferEnabled property is set to true, instead of writing log entries immediately to the various logging targets (like console or file), the logger first stores them in a buffer. This buffering process makes use of the Channel class to hold the log entries.

1. Advantages of Buffering:

   Performance Improvement: In some scenarios, writing logs to their destination can be a slow operation, especially when the destination is a remote server or a slow disk. By using a buffer, the application can continue its main operation without waiting for the log writing to complete. The actual writing can then be done in the background.

   Asynchronous Processing: With the buffer in place, logs can be processed asynchronously. This is evident from the use of the Loop method which continuously reads messages from the Buffer and logs them. This way, the main application isn't blocked by the logging process.
2. Working:

   - When BufferEnabled is set to true, the Buffer is initialized (if it hasn't been already).
   - Any log entry that needs to be logged is put into the buffer using the PutInQueue method.
   - The Loop method continuously checks the buffer for new log entries. When it finds a log entry, it sends the log message to all the logger targets in the loggerTargets list.
3. When is the Buffer used? The PutInQueue method decides whether to put the log entry into the buffer or to log it immediately. If bufferEnabled is true, the log entry is added to the buffer; otherwise, it's logged immediately.
4. Things to Note: Debugging: The logger has a DebugLogger flag. When this is set to true, the logger will also print debug trace logs that show the internal working of the logger. This is particularly useful for troubleshooting.
5. Threading: Given that the logger might be used from multiple threads, locks and channels are used to ensure thread safety.

In Summary:
The BufferEnabled mode in the iLogger class provides a way to buffer log entries for asynchronous processing, potentially improving the performance of the application by decoupling the log-writing process from the main operation of the application.

**Buffer Disabled Mode**

The use of `BufferEnabled` set to `false`, meaning immediate or synchronous logging, might be preferable in some scenarios:

1. **Critical Systems** : In systems where logs are critically important, such as financial transactions, you'd want to be certain that every transaction is logged immediately. The potential danger of buffering in such a scenario is data loss in case of system failures or crashes before buffered logs have been flushed.
2. **Debugging during Development** : When developing and debugging, it's often easier to have logs written out immediately to understand the flow of the program in real-time.
3. **Low Volume Logging** : If your application doesn't generate a large volume of log messages, then the performance benefit of asynchronous logging might be negligible. In such cases, synchronous logging (i.e., `BufferEnabled` set to `false`) simplifies the process.
4. **Avoiding Memory Overhead** : Buffering logs requires memory. If you're running in a resource-constrained environment, it might be beneficial to log directly rather than buffer.
5. **Real-time Monitoring** : If you have a monitoring system or a logging tool that's analyzing logs in real-time, then asynchronous logging might introduce unwanted delays.
6. **Short-lived Processes** : For processes that start up, do their work, and shut down quickly (e.g., certain batch jobs or scripts), buffering might not provide much benefit, and there could be a risk that the buffer doesn't get completely flushed before the process exits.
7. **Simpler Error Handling** : If there's an issue with writing a log (e.g., a disk is full), you might want to know immediately, so you can handle the error right where it happens. With buffering, the error might occur asynchronously, making it a bit trickier to handle.
8. **Ensuring Sequential Order** : In heavily multi-threaded environments, buffering might introduce a scenario where logs from different threads get mixed in a non-sequential manner. Direct logging ensures that logs are written in the exact sequence they occur.

While there are benefits to synchronous logging, remember that it does come at the potential cost of performance. In scenarios where performance is a critical concern, and the volume of logs is high, asynchronous logging (i.e., `BufferEnabled` set to `true`) would likely be the better choice. But, as with many things in software design, it's all about evaluating and balancing trade-offs based on the specific needs of your application.

🔜 Coming Next
--------------

Stay tuned for our upcoming feature on context targeting. Soon, you'll have even greater control over your logs with the ability to specify the contexts where logging should happen.

Join the revolution and experience the new era of logging in C# with iLogger today!

🔐 License
----------

* This project is licensed under the Apache V2.0 for free software use - see the [LICENSE](./LICENSE-APACHE.txt) file for details.
* For commercial software use, see the [LICENSE\_NOTICE](./LICENSE_NOTICE.md) file.

📬 Contact
----------

If you have any questions, suggestions, or just want to chat about iLogger, please feel free to reach out to us:

* [Tec-Net](mailto:tecnet.paris@gmail.com)

