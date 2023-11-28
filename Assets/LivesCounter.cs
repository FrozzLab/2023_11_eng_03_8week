using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesCounter : MonoBehaviour
{
	private TMP_Text _text;
	[SerializeField] private Health health;
	
    // Start is called before the first frame update
    void Start()
    {
	    _text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
	    _text
		    .SetText( "x "+health.Current);
    }
}
