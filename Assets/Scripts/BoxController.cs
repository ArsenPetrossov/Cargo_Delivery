using System.Collections;
using UnityEngine;
using Obi;

public class BoxController : MonoBehaviour
{
    [SerializeField] private GameObject _rope;
    [SerializeField] private float _speed = 1;
    [SerializeField] private RopeController _ropeController;

    private ObiParticleAttachment _ropeAttachment;

    private void Start()
    {
        _ropeController.RopeOverFinish += DropDown;
    }

    private void OnDestroy()
    {
        _ropeController.RopeOverFinish -= DropDown;
    }

    private void Awake()
    {
        var ropeAttachments = _rope.GetComponents<ObiParticleAttachment>();
        foreach (var ropeAttachment in ropeAttachments)
        {
            if (ropeAttachment.target.gameObject == gameObject)
            {
                _ropeAttachment = ropeAttachment;
                break;
            }
        }
    }

    private void OnCollisionEnter()
    {
        _ropeAttachment.enabled = false;
    }

    public void DropDown(Vector3 position)
    {
        _ropeAttachment.attachmentType = ObiParticleAttachment.AttachmentType.Static;
        GetComponent<ObiRigidbody>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(position.x, transform.position.y, transform.position.z);
        StartCoroutine(DeliverToTarget(position));
    }

    private IEnumerator DeliverToTarget(Vector3 targetPosition)
    {
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
            yield return null;
        }
        _ropeAttachment.enabled = false;
    }
}