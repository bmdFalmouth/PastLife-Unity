using UnityEngine;
using System.Collections;



public class PlayerController : MonoBehaviour {

	public float speed=1.0f;
	public float jumpSpeed=2000.0f;
	Animator anim;
	bool ground=false;
	// Use this for initialization
	void Start () {
		anim=GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetFloat("speed",Mathf.Abs(Input.GetAxisRaw("Horizontal")));

		if (ground){
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
				rigidbody2D.AddForce(new Vector2(0.0f,jumpSpeed));
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag=="ground")
		{
			ground=true;
			anim.SetBool("jump",false);
		}
	}
}
