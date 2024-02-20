using Photon.Pun;
using UnityEngine;

public class PhotonCastomTransformView : MonoBehaviour, IPunObservable
{
    private Vector2 pos;
    PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        pos = transform.position;
        //_networkPosition = (Vector2)transform.position;
        //_lastPositionNetwork = (Vector2)transform.position; 
    }

    private void Update()
    {
        if (_photonView.IsMine) return;
        //timeSerialization += Time.deltaTime;
        //transform.position = Vector3.Lerp(_lastPositionNetwork, _futurePosition, timeSerialization * PhotonNetwork.SerializationRate);
        var a = Vector3.Distance(transform.position, pos);
        transform.position = Vector3.MoveTowards(transform.position, pos, a * Time.deltaTime * PhotonNetwork.SerializationRate);//Vector2.Lerp(transform.position, pos, 0.1f);
    }

    //Vector2 _networkPosition;
    //Vector2 _lastPositionNetwork;
    //Vector2 _futurePosition;
    //float timeSerialization = 0;


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) 
        {
            var x = transform.position.x + 8f;
            byte x_wtiting = (byte)(x * 255f / 16f);

            var y = transform.position.y + 5f;
            byte y_wtiting = (byte)(y * 255f / 10f);

            stream.SendNext(x_wtiting);
            stream.SendNext(y_wtiting);
        }
        else
        {
            var x_read = (byte)stream.ReceiveNext();
            float x = (x_read * 16f / 255f) - 8f;
            var y_read = (byte)stream.ReceiveNext();
            float y = (y_read * 10f / 255f) - 5f;

            //_networkPosition = new Vector2(x, y);
            //var directions = _networkPosition - _lastPositionNetwork;
            //_lastPositionNetwork = _networkPosition;
            //_futurePosition = _networkPosition + directions;
            //Debug.Log(timeSerialization);
            //timeSerialization = 0f;

            pos = new Vector2(x, y);
        }
    }

}

