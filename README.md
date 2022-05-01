# Making a Jumper agent

This tutorial will walk you through the process of making a Jumper agent from scratch.

![Jumper Intro](/images/Jumper_intro.PNG)

The object of this tutorial is to teach the agent to jump over the obstacles and catch coins.

## Overview

In this tutorial we will:

1. Create an environment for the agent. This will contain all the objects as well all the physical simulations.
2. Implement the Agent subclasses. This will contain the logic for the agent. The agent will be able to learn and act.
3. Add the Agent subclasses to appropriate GameObjects.

If you are unfamiliar with Unity and ML-Agent you should get yourself familiarized with it before starting this tutorial.

## Set Up The Unity Project

First you will have to create a Unity project in the Unity Hub and than add ML-Agent assets to the project.

1. Launch Unity Hub, create a 3D project and name it however you want.
2. Add ML-Agent package in the projects packet manager. Make sure it's the latest version.

It should look like this:

![CreateProject](/images/CreateProject.PNG)
![MLPackage](/images/MLPackage.PNG)

## Create Environment

Now we will create the scene. It will include a Plane that will act as our floor, a small cube which will be our agent, a long cube which will be our obstacle, an another small cube which will act as a bonus reward and a wall that will destroy the obstacle and bonus when they collide.

### Create the Floor plane

1. Go into the hirachy area on the left and right click.
2. Select > 3D Object > Plane.
3. Name the GameObject as "Floor" and select it.
4. Now change the properties as shown below.

Postion = (0,0,0), Rotation = (0,0,0) and Scale = (10,1,10).

![Floor](/images/Floor.PNG)

### Create the Agent cube

1. Go into the hirachy area on the left and right click.
2. Select > 3D Object > Cube.
3. Name the GameObject as "Agent" and select it.
4. Now change the properties as shown below.

Postion = (0,0.5,0), Rotation = (0,-90,0) and Scale = (1,1,1).

![Agent](/images/Agent.PNG)

### Create the Obstacle

1. Go into the hirachy area on the left and right click.
2. Select > 3D Object > Cube.
3. Name the GameObject as "ObstacleSpawner" and select it.
4. Now change the properties as shown below.

Postion = (-15,0.5,0), Rotation = (0,0,0) and Scale = (1,1,1)

![Obstacle](/images/Obstacle.PNG)

### Create the Wall

1. Go into the hirachy area on the left and right click.
2. Select > 3D Object > Cube.
3. Name the GameObject as "Wall" and select it.
4. Now change the properties as shown below.

Postion = (15,5,0), Rotation = (0,0,0) and Scale = (10,10,10)

![Wall](/images/Wall.PNG)

### Create the Bonus reward

1. Go into the hirachy area on the left and right click.
2. Select > 3D Object > Cube.
3. Name the GameObject as "BonusSpawner" and select it.
4. Now change the properties as shown below.

Postion = (0.04,4.3,0), Rotation = (0,0,0) and Scale = (1,1,1)

![Bonus](/images/Bonus.PNG)

### Group the objects into a Training Area

By creating a training area it wil simplify some steps in the future.

1. Go into the hirachy area on the left and right click.
2. Select > empty object.
3. Name the GameObject as "Training Area" and select it.
4. Reset the Training Area so that it is Postion = (-602.4,0,0), Rotation = (0,0,0) and Scale = (1,1,1).
5. Now drag the Floor, Agent, Obstacle, Wall and Bonus object into the Training Area.

It should look like this:

![TrainingArea](/images/TrainingArea.PNG)

## Scripts

### Agent Script

To create the Agent script:

1. Select the Agent Object.
2. Click Add Component.
3. Click New Script.
4. Name the script as "Agent".
5. Click Create and Add.

Now we will edit the Agent script:

1. Double click the agent script to open it.
2. Import the needed ML-agent package by adding these:

```
using UnityEngine;
using Unity.MLAgents.Actuators;
```

3. Change the moodbevhaviour to Unity.MLAgents.Agent.
4. Delete Update(), but keep Start().

Now we will need to extend four methods from the Agent base class.

- OnEpisodeBegin()
- OnActionReceived(ActionBuffers actionBuffers)
- OnTriggerEnter(Collider other)
- OnCollisionEnter(Collision other)

#### OnEpisodeBegin()

Training in the ML-Agents Toolkits involves training in episodes, when an Agent solves the task, fails or times out the episode will start again. But to start an episode we will need to use the method `OnEpisodeBegin()`, this method will set-up the environment again for a new episode.

In this example, each time the Agent fails or succeeds the target is placed back to it's location and the variable `isGrounded` is set back to true.

At last we will also be adding a Rigidbody, this is Unity's primary element for physics simulation. To reference this we will be using ` GameObject.GetComponent<T>()` , which we will call in our ` Start()` method.

Our Agent script should look like this:

```
using UnityEngine;
using Unity.MLAgents.Actuators;

public class Agent : Unity.MLAgents.Agent
{

    Rigidbody rBody;
    public bool isGrounded = true;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        isGrounded = true;
        transform.localPosition = new Vector3(0, 0.5f, 0);
    }
}

```

#### Taking Actions and receiving rewards

For the final part we have to implement 3 methods. Which will all receive actions and assign rewards.

##### OnActionReceived(ActionBuffers actionBuffers)

The OnActionReceived() method should look like this:

```
public override void OnActionReceived(ActionBuffers actionBuffers)
{
    AddReward(0.001f);
    if (isGrounded && actionBuffers.DiscreteActions[0] == 1)
    {
        isGrounded = false;
        rBody.velocity = new Vector3(0, 10, 0);
        AddReward(-0.030f);
    }
}

```

##### OnTriggerEnter(Collider other)

// This method will be called when the Agent collides with ther object.

```

```

##### OnCollisionEnter(Collision other)

This method will be called when the Agent collides with the object. Mainly used to check if it collides with the Obstacle. When it does collide the agent will have a negative reward, be destroyed and the episode will restart.

The OnCollisionEnter() method should look like this:

```
void OnCollisionEnter(Collision other)
{
    if (other.gameObject.CompareTag("Obstacle"))
    {
        AddReward(-10.0f);
        Destroy(other.gameObject);
        EndEpisode();
    }
}
```

### Bonus Script

To create the Bonus script:

1. Select the BonusSpawner Object.
2. Click Add Component.
3. Click New Script.
4. Name the script as "Bonus".
5. Click Create and Add.

Now we will need to extend three methods from the Agent base class.

- Start()
- Update()
- OnTriggerEnter(Collider other)

#### Start()

In the Start method we will set the speed to a random value between 3f and 5f. So each time the Bonus object is spawned it will have a different speed.

```
private float speed;
private void Start()
{
    speed = Random.Range(3f, 5f);
}
```

#### Update()

In the update method we will be moving the Bonus object forward at the speed we set in the Start() method.

```
void Update()
{
    transform.Translate(Vector3.right * speed * Time.deltaTime);
}
```

#### OnTriggerEnter(Collider other)

In this method we will check if the Bonus object collides with the Wall object. If it does we will destroy the Bonus object.

```
private void OnTriggerEnter(Collider other)
{
    if (other.gameObject.CompareTag("Wall") == true)
    {
        Destroy(gameObject);
    }
}
```

### Obstacle Script

To create the Obstacle script:

1. Select the ObstacleSpawner Object.
2. Click Add Component.
3. Click New Script.
4. Name the script as "Obstacle".
5. Click Create and Add.

Now we will need to extend three methods from the Agent base class.

- Start()
- Update()
- OnTriggerEnter(Collider other)

#### Start()

In the Start method we will set the speed to a random value between 3f and 5f. So each time the Obstacle object is spawned it will have a different speed.

```
private float speed;
private void Start()
{
    speed = Random.Range(3f, 5f);
}
```

#### Update()

In the update method we will be moving the Obstacle object forward at the speed we set in the Start() method.

```
void Update()
{
    transform.Translate(Vector3.right * speed * Time.deltaTime);
}
```

#### OnTriggerEnter(Collider other)

In this method we will check if the Obstacle object collides with the Wall object. If it does we will destroy the Obstacle object.

```
private void OnTriggerEnter(Collider other)
{
    if (other.gameObject.CompareTag("Wall") == true)
    {
        Destroy(gameObject);
    }
}
```

### Spawner Script

To create the Spawner script:

1. Go to the script folder.
2. Right click and add script C#.
3. Name the script as "Spawner".

Now we will need to extend three methods from the Agent base class.

- Start()
- IEnumerator SpawnObject()
- Spawn( )

#### Start()

In the Start method we will imply a method calles StartCourtine(SpawnObject) which will call the SpawnObject() method.

```
void Start()
{
    StartCoroutine(SpawnObject());
}

```

#### IEnumerator SpawnObject()

In this method we will be returning a IEnumerator which will be used to call the Spawn() method.

```
IEnumerator SpawnObject()
{
    yield return new WaitForSeconds(Random.Range(4f, 6.5f));
    Spawn();
}

```

#### Spawn()

In this method we will be spawning the object by setting the position of the object and its parent object.

```
public GameObject ObjectToSpawn;
void Spawn()
{
    var spawnedObject = Instantiate(ObjectToSpawn);
    spawnedObject.transform.parent = transform.parent;
    spawnedObject.transform.localposition = transform.localposition;
}

```

All the scripts should be done.

## Final Agent Setup

Now that all the Gameobjects and Ml-Agent components are done we need to edit some properties of the Agent object.

1. Select the Agent Object and show its properties in the inspector window.
2. Add a Rigidbody component to the Agent object if it not already added.
3. Add a Behavoir component, name it Jumper.
4. Add a Decision Requester component and set the decision period to 5.
5. Add a Demonstration Recorder component to the Agent object.

The Agent object should look like this:

![Agent2](/images/Agent2.PNG)

Finaly we will be adding the ray perception component to the Agent object.

First we will add the ray component as a child object to the Agent object.

1. Right click on the Agent object in the hirachy and click "Create empty".
2. Name the object as "Rays".

Now add the RayPerception Sensor 3D component to the Rays object and set the parameters.

![Rays](/images/Rays.PNG)

Finaly add the RayPerception Sensor 3D component to the Agent object and set the parameters.

![RayAgent](/images/RayAgent.PNG)

## Testing the Environment

Before training it is always good to first test your environment by controlling the Agent using the keyboard. To be able to do this we will need to add a method called `Heuristic()` to the Agent script.
The method should look like this:

```
public override void Heuristic(in ActionBuffers actionsOut)
{
    var discreteActionsout = actionsOut.DiscreteActions;
    if (Input.GetKey(KeyCode.Space) == true)
        discreteActionsout[0] = 1;
}
```

In order for the Agent to use the Heuristic, we will need to set the Behavior Type to "Heuristic Only".
Now you van press run and move the Agent using the arrow keys.

## Training the Environment
