using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed=1.0f;
	Animator anim;
	// Use this for initialization
	void Start () {
		anim=GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetFloat("speed",Mathf.Abs(Input.GetAxisRaw("Horizontal")));


		if (Input.GetAxisRaw("Horizontal")>0)
		{
			transform.eulerAngles=new Vector2(0.0f,0.0f);
			transform.Translate(new Vector3(1.0f,0.0f,0.0f)*speed*Time.deltaTime);
		}
		else if(Input.GetAxisRaw("Horizontal")<0)
		{
			transform.eulerAngles=new Vector2(0.0f,180.0f);
			transform.Translate(new Vector3(1.0f,0.0f,0.0f)*speed*Time.deltaTime);
		}

		if (Input.GetButtonDown("Jump"))
		{
			anim.SetBool("jump",true);
			rigidbody2D.AddForce(new Vector2(0.0f,200.0f));
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag=="ground")
		{
			anim.SetBool("jump",false);
		}
	}
}
