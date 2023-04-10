# Drone Delivery Service
A squad of drones is tasked with delivering packages for a major online reseller in a world
where time and distance do not matter. Each drone can carry a specific weight and can make
multiple deliveries before returning to home base to pick up additional loads; however, the goal
is to make the fewest number of trips as each time the drone returns to home base, it is
extremely costly to refuel and reload the drone.

The purpose of the written software is to accept input which will include the name of each
drone and the maximum weight it can carry, along with a series of locations and the total weight
needed to be delivered to that specific location. The software should highlight the most efficient
deliveries for each drone to make on each trip.

Assume that time and distance to each drop off location do not matter, and that the size of
each package is also irrelevant. It is also assumed that the cost to refuel and restock each
drone is a constant and does not vary between drones. The maximum number of drones in a
squad is 100, and there is no maximum number of deliveries which are required.

# Walk Through Solution
This solution consisted of eliminating the locations and drones that had negative weights and then 
the locations were ordered ascendingly based on the weight of their packages. Then for each drone, 
it was verified which packages it could carry and which it could not, based on its load capacity. 
Then the trips were calculated by adding package by package until the drone's capacity was filled, 
which indicated that a new trip should be scheduled, if there were still packages to be delivered.
The delivery plan for a drone finished when all the packages are added.

# Technical Dependencies and Libraries
This solution was developed with C# on .Net 6.0, using Visual Studio 2022 as the IDE.
