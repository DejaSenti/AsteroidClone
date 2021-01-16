using UnityEngine;

public class ExplosionParticleSystem : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private PlayerShipManager playerShipManager;
    [SerializeField]
    private new ParticleSystem particleSystem;
#pragma warning restore 0649

    public void Initialize()
    {
        playerShipManager.playerShip.PlayerShipCollisionEvent.AddListener(OnEntityDestroyedEvent);
        AlienShipManager.AlienShipDestroyedEvent.AddListener(OnEntityDestroyedEvent);
        AsteroidManager.AsteroidDestroyedEvent.AddListener(OnEntityDestroyedEvent);
    }

    private void OnEntityDestroyedEvent(SpaceEntity spaceEntity, string destroyerTag)
    {
        float particleScale = 1;
        if (spaceEntity.transform.localScale.x != 1)
        {
            particleScale = Mathf.Sqrt(spaceEntity.transform.localScale.x / 2);
        }

        particleSystem.transform.localScale = new Vector3(particleScale, particleScale, 1);
        particleSystem.transform.position = spaceEntity.CorporealForm.transform.position;
        particleSystem.Play();
    }

    public void Terminate()
    {
        playerShipManager.playerShip.PlayerShipCollisionEvent.RemoveListener(OnEntityDestroyedEvent);
        AlienShipManager.AlienShipDestroyedEvent.RemoveListener(OnEntityDestroyedEvent);
        AsteroidManager.AsteroidDestroyedEvent.RemoveListener(OnEntityDestroyedEvent);
    }
}
