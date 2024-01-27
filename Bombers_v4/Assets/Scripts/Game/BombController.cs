using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    //[SerializeField] BombManager _bombManager;
    [SerializeField] GameObject _bomb;

    [SerializeField] CharacterTraits characterTraits;

    //private void Start()
    //{
    //    //characterTraits = gameObject.GetComponent<CharacterTraits>();
    //    //var _bombScript = _bomb.GetComponent<Bomb>();
    //    //_bombScript.FlameLength = characterTraits.FlameLength;
    //    //characterTraits.OnFlameLengthChanged += (l) => _bombScript.FlameLength = l;
    //}

    private readonly List<GameObject> _bombs = new ();
    
    private void Update()
    {
        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    _bombs.RemoveAll(x => x == null);
        //    if (characterTraits.bombQuantity > _bombs.Count)
        //    {
        //        _bombs.Add(_bombManager.SetBomb(_bomb, transform.position));
        //    }
        //}
    }


}
