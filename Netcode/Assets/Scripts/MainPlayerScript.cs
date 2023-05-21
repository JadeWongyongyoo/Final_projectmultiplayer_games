using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using Unity.Collections;

public class MainPlayerScript : NetworkBehaviour
{
    public float speed = 2.0f;
    
    Rigidbody rb;

    public TMP_Text namePrefab;
    private TMP_Text nameLabel;
    public NetworkVariable<int> postX = new NetworkVariable<int>(0,
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public NetworkVariable<NetworkString> playerNameA = new NetworkVariable<NetworkString>(
        new NetworkString { info = "Player A" },
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    public NetworkVariable<NetworkString> playerNameB = new NetworkVariable<NetworkString>(
        new NetworkString { info = "Player B" },
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    private LoginManagerScript LoginManager;

    public struct NetworkString : INetworkSerializable
    {
        public FixedString32Bytes info;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref info);
        }
        public override string ToString()
        {
            return info.ToString();
        }
        public static implicit operator NetworkString(string V) => 
            new NetworkString() { info = new FixedString32Bytes(V)};
    }

    public override void OnNetworkSpawn()
    {
        GameObject canvas = GameObject.FindWithTag("MainCanvas");
        nameLabel = Instantiate(namePrefab, Vector3.zero, Quaternion.identity) as TMP_Text;
        nameLabel.transform.SetParent(canvas.transform);


        if (IsOwner)
            LoginManager = GameObject.FindObjectOfType<LoginManagerScript>();
        if (LoginManager != null)
        {
            string name = LoginManager.userNameInput.text;
            if (IsOwnedByServer) { playerNameA.Value = name; }
            else { playerNameB.Value = name; }
        }
    }



    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (IsOwner)
        {
            float translation_1 = Input.GetAxis("Horizontal") * speed;
            
            translation_1 *= Time.deltaTime;
         
            transform.position += new Vector3(translation_1,0, 0);
            
        }
    }

    private void Update()
    {
        Vector3 nameLabelPos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2.5f, 0));

        nameLabel.text = gameObject.name;
        nameLabel.transform.position = nameLabelPos;

        if (IsOwner)
        {
            postX.Value = (int)System.Math.Ceiling(transform.position.x);
        }
        updatePlayerInfo();
    }

    private void updatePlayerInfo()
    {
        if (IsOwnedByServer)
        {
            nameLabel.text = playerNameA.Value.ToString();
        }
        else
        {
            nameLabel.text = playerNameB.Value.ToString();
        }
    }

    public override void OnDestroy()
    {
        if(nameLabel != null)
        {
            Destroy(nameLabel.gameObject);
        }
        base.OnDestroy();
    }
}
