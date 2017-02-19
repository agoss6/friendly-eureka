using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This is an alternative method to collect results from several experiment runs. The object logger collects a data point every frame but this class collects a data point only at
/// remarkable events like a hit or helicopter detection. The load on the cpu is thus greatly reduced without losing any vital data. Also, the data is in a form that makes it east to graph.
/// There is a static list that records the data from each run of the experiment. Because the list is static it sticks around through the entire simulation
/// The static list contains "ExperimentRuns." Each ExperimentRun has attributes like the shooter's location, helicopter model, TOD, experiment number , and a list of the hits.
/// Each ExperimentRun also has a list of "Hits" to keep track of all the Hits on the helicopter in that run.
/// A Hit is a Vector3 that points to the helicopter's location when it was hit.
///
/// The results screen plots an experiment n by indexing into List<ExperimentRun>[n-1], putting a sphere at the observer's location, then iterating through List<Hits> and putting a cube
/// at each hit location.
/// </summary>
public class ResultsCollector : MonoBehaviour {

	private int bulletHits;					// keeps track of the number of bullet hits

	public GameObject theActor;				// the helicopter
	private Transform heliLocation;			// the helicopter's location

	public GameObject theObserver;			// the shooter
	private Transform observerLocation;		// the shooter's location

	private SceneQueue theQueue;			// the scene queue- we want this for theQueue.sceneCounter
	private int experimentNumber;			// theQueue.sceneCounter

	public static List<ExperimentRun> experimentRuns = new List<ExperimentRun>();	// the static list of ExperimentRuns, default is empty

	// Used for initialization
	void Start () {
		bulletHits = 0;
		theQueue = FindObjectOfType<SceneQueue> ();		// Expensive operation: want to do only once
		experimentNumber = theQueue.sceneCounter;			// The current experiment you are running

		// loactions of the helicopter and observer
		heliLocation = theActor.transform;
		observerLocation = theObserver.transform;

		// Methods assigned to the aircraftHit delegate
		GunHandler.aircraftHit += IncrementBulletHits; 	// keeps track of the number of hits
		GunHandler.aircraftHit += StoreHitInfo;					// stores the location of a hit
	}

	void OnDestroy(){
		// Methods un-assigned to the aircraftHit delegate
		GunHandler.aircraftHit -= IncrementBulletHits;
		GunHandler.aircraftHit -= StoreHitInfo;
	}

	// Called every frame- creates a new ExperimentRun and adds it to the static list when the experimentNumber increases, which is once per scene.
	void Update(){
		experimentNumber = theQueue.sceneCounter;
			// A new experimentRun is created only once per scene, though the if statement is tested every frame
			if (experimentRuns.Count < experimentNumber) {
				int modelNumber = theQueue.getScene (theQueue.getCorrectSceneList()).model;		// which helicopter
				int tod = theQueue.getScene (theQueue.getCorrectSceneList()).tod;							// Time Of Day
				ExperimentRun newExperimentRun = new ExperimentRun (experimentNumber, observerLocation.position, modelNumber, tod);
				experimentRuns.Add (newExperimentRun);
			}
	}

	void IncrementBulletHits(){
		bulletHits += 1;
	}

	// Create the new hit object and add it to the experiment run.
	void StoreHitInfo(){
		// check that there is both a shooter and a helicopter to avoid null reference exceptions
		if (theObserver != null && theActor != null) {
			heliLocation = theActor.transform;
			Vector3 newHit = heliLocation.position;
			experimentRuns [experimentNumber - 1].addHit (newHit);						// need this -1 because the nth experiment run corresponds to the n-1st object in the list
		}
	}

	// getter method for the static list
	public List<ExperimentRun> getExperimentRuns(){
		return experimentRuns;
	}
}
