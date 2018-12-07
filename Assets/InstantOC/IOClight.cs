using UnityEngine;
using System.Collections.Generic;

public class IOClight : MonoBehaviour {
	
	public int Probes;
	public float ProbeRadius;
	
	private int probes;
	private float probeRadius;
	private GameObject go;
	private RaycastHit hit;
	private SphereCollider probe;
	private Vector3 center;
	private float range;
	private float angle;
	private Ray ray;
	private Vector3 rayDir;
	private int counter;
	private IOCcam iocCam;
	private int frameInterval;
	private bool hidden;
	private Transform parent;
	private int currentLayer;
	private Vector2 rndPoint;
	private GameObject prefab;
	
	void Awake () {
		iocCam =  Camera.main.GetComponent<IOCcam>();
		if(iocCam == null)
		{
			this.enabled = false;
		}
		else
		{
			hit = new RaycastHit();
			currentLayer = gameObject.layer;
			
		}
	}
	
	void Start () {
		UpdateValues();
		prefab = Resources.Load("probe") as GameObject;
		prefab.GetComponent<SphereCollider> ().radius = probeRadius;
		center = transform.position;
		GetComponent<Light>().enabled = false;
		GetComponent<Light>().renderMode = LightRenderMode.ForcePixel;
		range = GetComponent<Light>().range;
		angle = GetComponent<Light>().spotAngle;
		parent = transform;
		switch(GetComponent<Light>().type)
		{
			case LightType.Point:
				for(int i=0;i<probes;i++)
				{
					ray = new Ray(center, Random.onUnitSphere);
					if(Physics.Raycast(ray, out hit, range))
					{
						go = Instantiate(prefab, hit.point, Quaternion.identity) as GameObject;
						go.transform.parent = parent;
						go.layer = currentLayer;
					}
				}
			break;
			
			case LightType.Spot:
				for(int i=0;i<probes;i++)
				{
					rndPoint = Random.insideUnitCircle * (Mathf.Tan(Mathf.Deg2Rad * angle * 0.5f) * range);
					rayDir = ((center + parent.forward * range + parent.rotation * new Vector3(rndPoint.x, rndPoint.y)) - center).normalized;
					ray = new Ray(center, rayDir);
					if(Physics.Raycast(ray, out hit, range))
					{
						go = Instantiate(prefab, hit.point, Quaternion.identity) as GameObject;
						go.transform.parent = parent;
						go.layer = currentLayer;
					}
				}
			break;
		}
	}
	
	public void UpdateValues () {
		if(Probes != 0)
		{
			probes = Probes;
		}
		else probes = iocCam.lightProbes;
		if(ProbeRadius != 0)
		{
			probeRadius = ProbeRadius;
		}
		else probeRadius = iocCam.probeRadius;
	}
	
	public void UnHide() {
		counter = Time.frameCount;
		hidden = false;
		GetComponent<Light>().enabled = true;
	}
	public void Hide() {
		hidden = true;
		GetComponent<Light>().enabled = false;
	}
	void Update() {
		frameInterval = Time.frameCount % 6;
		if(frameInterval == 0)
		{
			if(!hidden && Time.frameCount - counter > iocCam.hideDelay)
			{
				Hide();
			}
		}
	}
}
