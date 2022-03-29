# Making a Jumper agent

This tutorial will walk you through the process of making a Jumper agent from scratch.

![Jumper Intro](/images/Jumper_intro.png)

We will teach the agent to jump over the obstacles and catch coins.

## Overview

1. Create an environment for the agent.
2.

## Set Up The Unity Project

First you will have to create a unity project in the unity hub and than add ML-Agent assets to the project.

1. Launch Unity Hub, create a 3D project and name it however you want.
2. Add ML-Agent package in the projects packet manager. Make sure it's the latest version.

It should look like this.

![CreateProject](/images/CreateProject.png)![MLPackage](/images/MLPackage.png)

## Create Environment

Now we will create the scene. It will include a Plane that will act as our floor, a small cube which will be our agent, a long cube which will be our obstacle and another small cube which will act as a bonus reward.

### Create the floor

1. Go into the hirachy area on the left and right click.
2. Select > 3D Object > Plane.
3. Name the GameObject as "Floor" and select it.
4. Now change the properties as shown below.

Postion = (0,0,0), Rotation = (0,0,0) and Scale = (10,1,10).

![Floor](/images/Floor.png)

### Create the agent

1. Go into the hirachy area on the left and right click.
2. Select > 3D Object > Cube.
3. Name the GameObject as "Agent" and select it.
4. Now change the properties as shown below.

Postion = (0,0.5,0), Rotation = (0,-90,0) and Scale = (1,1,1).

![Agent](/images/Agent.png)

### Create the obstacle

1. Go into the hirachy area on the left and right click.
2. Select > 3D Object > Cube.
3. Name the GameObject as "Obstacle" and select it.
4. Now change the properties as shown below.

Postion = (-10,0.5,0), Rotation = (0,0,0) and Scale = (1,1,5)

![Obstacle](/images/Obstacle.png)

### Create the bonus

1. Go into the hirachy area on the left and right click.
2. Select > 3D Object > Cube.
3. Name the GameObject as "Bonus" and select it.
4. Now change the properties as shown below.

Postion = (0.04,4.3,0), Rotation = (0,0,0) and Scale = (1,1,1)

![Bonus](/images/Bonus.png)
