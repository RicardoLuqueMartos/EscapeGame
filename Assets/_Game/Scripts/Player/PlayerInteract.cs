using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactDistance = 1f;
    [SerializeField] private float SphereDiameter = .1f;
    [SerializeField] private float distanceStopInteract = 1f;
    [SerializeField] private LayerMask interactable;

    private RaycastHit hit;
    private iInteractable currentTarget;
    public iInteractable interactingTarget;

    public bool isInteracting = false;
    Ray ray;

    private void Update()
    {
        RayDetectInteractable();
    }

    void FixedUpdate()
    {
        LeaveInteractByDistance();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(ray);
    }

    void RayDetectInteractable()
    {
        if (interactingTarget != null && Input.GetKeyDown(KeyCode.E))
        {
            LeaveInteractByKey();
            return;
        }
        RaycastHit hit;
        if (Physics.SphereCast(transform.position,
            SphereDiameter, transform.forward, out hit, interactDistance, interactable))
        {
            if (hit.collider != null)
            {
                currentTarget = hit.collider.GetComponent<iInteractable>();

                if (currentTarget != null)
                {
                    currentTarget.IsInteractable(hit);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log(hit.transform.name);

                        if (!currentTarget.isControlled())
                        {
                            if (interactingTarget != null)
                            {
                                interactingTarget.LeaveInteract();
                            }
                            currentTarget.Interact(this);
                        }
                        else
                        {
                            currentTarget.LeaveInteract();
                        }
                    }
                }
                else
                {
                    UIManager.instance.HideInfoText();
                }
            }
        }

        else if (UIManager.instance != null && !isInteracting)
        {
            currentTarget = null;
            UIManager.instance.InteractedInfoText("");
            UIManager.instance.HideInfoText();
        }
    }

    void LeaveInteractByKey()
    {
        interactingTarget.LeaveInteract();
        interactingTarget = null;
    }

    void LeaveInteractByDistance()
    {
        if (isInteracting && interactingTarget != null &&
            Vector3.Distance(this.transform.position, ((MonoBehaviour)interactingTarget).transform.position) > distanceStopInteract)
        {
            interactingTarget.LeaveInteract();
            interactingTarget = null;
        }
    }
}
