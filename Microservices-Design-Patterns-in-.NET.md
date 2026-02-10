# Microservices Design Patterns in .NET

## Chapter 01 - Introducing Microservices – the Big Picture

> Microservices are small, autonomous services that work together.

### Approaches to software development

#### 1. Monolith or Monolothic Application

- Unified software system in which all components and functionalities are interwoven.
- N-tier architecture as a design pattern
- Separation of concerns into logical layers.
- All functionality are part of the same code base  

- Scaling difficulties
- Limited technology flexibility
- Deployment bottlenecks
- Code debt

#### 2. Modular Monolith
Divided the app code base into disting self-contained modules, each responsible for a specific business domain.

- Separation of concerns
- Independent development and testing
  

#### 3. Microservices
Architectural style, breaks the application into small, independent services that communicates through APIs

- Independence
- Tech diversity
- Decentralized data management and efficient data storage
- Deployment - Docker
- Individual services
- Availability & Scalability
- Monitoring 
- Development speed

Design Patterns
- Sync and Async communication
- Data consistency (Saga pattern)
- Resilence, security and infrastructure patterns
- Cloud Dev patterns

.NetCore
- Cross platform compatibility
- High performance
- Modular and lightweight
- Support containerization

## Chapter 02 - Applying Domain-Driven Design Principles
### DDD
> The primary objective is to model software that accurately mirros the problem domain, making it easier for developers to understand the application's intricacies and adapt to new requirements.

- Entity: Represents a distinct object withing the domain, characterized by a unique identity and a life cycle. Is a representation of data in your system.
- Models: Are abstractions that define rules and behaviors of a domain and are used to solve domain problems. Is a central reference point in our design and development process and can then be grouped into logical modules.
- Ubiquitous language> This refers to language unique to a domain model and used by team members within activities related to the domain.
- Bounded Context: Is a logical boundary of a domain where terms and rules apply.
- Value objects: Defined solely by their attributes, not by identity. Unlike entities, they have no unique identifier and are considered equal if all their properties match. They are immutable, meaning their state cannot be changed after creation. Their keys are formed through the composition of the values of all their properties.
- Aggregates: Are clusters of related entities and value objects treated as a single unit to maintain consitency and enforce business rules. Each aggregate has a __root entity__ that is only the entry point for external access. All modification goes through the __aggregate root__

If your sofware is to be used in a bank, then the domain is banking. The domain is not the code or technology. It's the business logic, rules and concepts that matter most to the people using the software.

###### Pros
- DDD requires a substantial upfront investment in understanding and modeling the domain in detail.
- It emphasizes focusing on smaller, individual domain components
- Breaks our application into smaller segments.
- Promotes ubiquitous language, reducing misunderstandings.
- Ensures the the software design closely mirrors business processes.
- Cleaner code base.
- Improves flexibility and maintainability.

###### Cons
- Requires significant time and effort to understand the domain.
- Necessitates deep domain knowledge.
- Introduce unnecessary complexity for simple domains, making it less suitable for straightforward projects.

### DDD and microservices
Microservices architecture promotes dividing a monolithic application into self-contained and independent services.  
Each microservice serves its bounded context. Each will have its model, language, business rules and technology stack.  

DDD suggests separating the contexts into standalone components, microservices architecture is the perfect architectural pattern to support this ambition.  

It's vital to stablish a __central entity__ or __aggregate root__ for your system and design all other parts around it.

### The aggregate pattern
  
- Promotes the collection of related entities and aggregates them into a unit.
- Make it easier to define ownership of elements in a large system.
- Define consistency boundaries within which all changes must be made through a single, controlled entry point, the __aggregate root__
- Prevent accidental data corruption and business rule violations.
- Comprises one or more entities and value objects that interact in one way or another.
- Enforce specific rules for data and validation accross multiple objects easier
  ```csharp
  // Appointment class is the aggregate root
  public class Appointment
  {
      public Guid AppointmentId { get; set; } // agregate_root
      public Guid PatientId { get; set; } // related entity
      public Guid DoctorId { get; set; } // related entity
      public TimeSlot Slot { get; set; } // value-object entity
      public Location Location { get; set; } // value-object entity
      public string Purpose { get; set; } // value-object entity
  
      public void Reschedule(TimeSlot newSlot)
      {
          Slot = newSlot;
      }
  
      public void ChangePurpose(string newPurpose)
      {
          Purpose = newPurpose;
      }
  }
  ```

## Chapter 03 - Synchronous Communication between Microservices
## Chapter 04 - Asynchronous Communication between Microservices
Asynchronous communication in microservices
- __Message queues:__ Services (__*Publisher or Producer*__) send messages to queues, which are then processed by consuming services (__*Consumer*__). Preserve the exact order of messages (__*FIFO*__) and ensure each message is processed once.
  - e.g. Amazon Simple Queue Service (SQS), RabbitMQ, Azure Queue Storage, ActiveMQ.
- __Publish Subscribe (pub-sub) model:__ Services publish messages to an intermediary messaging system (_event bus_ or _message broker_) topics, and multiple subscribers receive these messages. Event-driven architecture.
  - command message: request some action to be performed.
  - event message: announce that some action took place. aimed at microservices that need to post or modify data.
  
  - Event Definition: Create event class that encapsulate the data to be transmitted
    - Event schema
    - Event type
    - Event versioning
    - Event metadata
  - Publishing events: a service emitting notifications about significant state changes within the system.
  - Subscribing to event: a service registers interest in particular event types.
    - At-most-once-delivery: a message is delivered only once. It may be lost, but it will never be duplicated. Useful for occasional message loss is acceptable.
    - At-least-once-delivery: a message is delivery one or more times. It will not be lost, but duplicates may occur. Consumer must implement idempotency.
    - Exactly once delivery: A message is delivery only once with no duplicates os loss.
  - Event handling: process or mechanisms by which services respond to events.
- __Event streaming:__ Implement continuos streams of events and services consume in real time.

###### RabbitMQ
- Producers and Consumers
  - Producers send messages
  - Consumers receive messages
- Exchanges and Queues
  - Producers send messages to exchanges, which route them to queues based on rules.
  - Consumers retrieve messages frome these queues.
- Bidings: association between exchanges and queues.
- Message routing: Various exchange types (direct, topic, fanout, headers) to control message routing mechanisms

## Chapter 05 - Working with the Aggregator Pattern

__Aggregator pattern__
- Facilitates data consolidation from multiple services into a single response.


## Chapter 06 - Working with the CQRS Pattern
## Chapter 07 - Applying Event-Sourcing Patterns
## Chapter 08 - Database Design Strategies for Microservices
## Chapter 09 - Implementing Transactions across Microservices Using the Saga Pattern
## Chapter 10 - Building Resilient Microservices
## Chapter 11 - Implementing the API and BFF Gateway Patterns
## Chapter 12 - Micro Frontends — Extending Microservices to the Frontend
## Chapter 13 - Securing Microservices
## Chapter 14 - Container Hosting and Development Patterns
## Chapter 15 - Serverless Microservices Development
## Chapter 16 - Observability and Monitoring with Modern Tools
## Chapter 17 - Wrapping It All Up
