using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The data structure for the ResultsCollector
/// </summary>
public class ExperimentRun : MonoBehaviour
{
	private int _experimentNumber;
	private Vector3 _theObserver;
	private int _modelNumber;	// 0 for Black Hawk (UH60), 1 for Raider (S97)

	private int _tod;	// 0 for dusk, 1 for day, 2 for night
	public Vector3 _minCoords;		// The first point on the helicopter's path
	public Vector3 _maxCoords;		// The last point on the helicopter's path
	private int _shotsFired;			// boom roasted

	public List<Vector3> _maybeHeardLocations;
	public Vector3 _heardLocation;
	public Vector3 _seenLocation;

	private List<Vector3> hits;	// stores all the hits in the run

	public ExperimentRun (int experimentNumber, Vector3 theObserver, int modelNumber, int tod)
	{
		_experimentNumber = experimentNumber;
		_theObserver = theObserver;
		_modelNumber = modelNumber;
		_tod = tod;
		_shotsFired = 0;
		hits = new List<Vector3> ();
		_minCoords = Vector3.zero;
		_maxCoords = Vector3.zero;
		_maybeHeardLocations = new List<Vector3> ();
	}

	public void addHit (Vector3 hit)
	{
		hits.Add (hit);
	}

	public void incrementShots ()
	{
		_shotsFired += 1;
	}

	public List<Vector3> getHitsList ()
	{
		return hits;
	}

	public int getExperimentNumber ()
	{
		return _experimentNumber;
	}

	public Vector3 getObserverLocation ()
	{
		return _theObserver;
	}

	public int getModelNumber ()
	{
		return _modelNumber;
	}

	public int getTOD ()
	{
		return _tod;
	}

	public int getShotsFired ()
	{
		return _shotsFired;
	}

	public void setMinCoords (Vector3 minCoords)
	{
		_minCoords = minCoords;
	}

	public void setMaxCoords (Vector3 maxCoords)
	{
		_maxCoords = maxCoords;
	}

	public void setHeard (Vector3 heardCoords)
	{
		_heardLocation = heardCoords;
	}
}
