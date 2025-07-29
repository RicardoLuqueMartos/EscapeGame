using UnityEngine;

public interface iInteractable
{

    public bool isControlled();

    public void IsInteractable(RaycastHit hit);

    public void Interact(PlayerInteract player = null);

    public void LeaveInteract();
}
