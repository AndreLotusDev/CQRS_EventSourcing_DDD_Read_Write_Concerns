## CQRS (Command Query Responsibility Segregation)

### Technologies Used
- Kafka

  Apache Kafka is a distributed streaming platform that is designed to handle real-time data feeds with high throughput and fault tolerance. It is commonly used for building real-time data pipelines and streaming applications.
  
  ### Key Concepts:
  
  - **Topics:** Kafka organizes data into topics, which are similar to a feed or a category for messages.
    
  - **Producers:** Producers are applications that push data into Kafka topics.
    
  - **Consumers:** Consumers are applications that read data from Kafka topics.
    
  - **Brokers:** Kafka runs as a cluster of one or more servers, called brokers, to manage the storage and distribution of data across topics.
    
  - **Partitions:** Topics are divided into partitions for scalability and parallelism.
    
  - **Replication:** Kafka allows replicating partitions across multiple brokers to ensure fault tolerance and high availability.
    
  ### Why Kafka?
  
  Kafka offers several benefits for building scalable and fault-tolerant data pipelines:
  
  - **Scalability:** Kafka can handle large volumes of data and scale horizontally by adding more brokers to the cluster.
    
  - **Durability:** Messages in Kafka topics are persisted to disk, ensuring data durability even in the event of broker failures.
    
  - **Low Latency:** Kafka's design prioritizes low latency message delivery, making it suitable for real-time data processing.
    
  - **Connectivity:** Kafka integrates well with various data sources and sinks, including databases, message queues, and streaming frameworks.

- How Kafka is used within your application architecture.
- Any customizations or configurations made to Kafka for your project.
- Dependencies on Kafka libraries or tools.
- Instructions for setting up Kafka in a development or production environment.

### Resources:

- [Apache Kafka Documentation](https://kafka.apache.org/documentation/)
- [Confluent Kafka Tutorials](https://docs.confluent.io/platform/current/tutorials/index.html)
  
- MongoDB
- .NET Core 7
- PostgreSQL

### Methodologies
- CQRS (Command Query Responsibility Segregation)
- DDD (Domain Driven Design)
- Event Sourcing

### Setup Instructions
1. Execute the provided `docker-compose` file to set up Kafka.
2. Run the Docker command for PostgreSQL to set up the database.
3. Run the Docker command for MongoDB to set up the database.

### Architecture Overview
For a detailed view of the architecture, please refer to the [Architecture Overview Diagram (PDF)](Architecture+Overview.drawio.pdf).

---

This repository implements the CQRS pattern along with other methodologies and technologies to provide a scalable and efficient system. If you have any questions or need further assistance, please refer to the documentation or reach out to the project maintainers.

---

<h1>Architecture overview</h1>

[Architecture+Overview.drawio.pdf](https://github.com/AndreLotusDev/CQRS_EventSourcing_DDD_Read_Write_Concerns/files/11009952/Architecture%2BOverview.drawio.pdf)
![Architecture+Overview drawio](https://user-images.githubusercontent.com/54090940/226147783-99560e8a-e02e-408c-a529-c82ad9dd303b.svg)
