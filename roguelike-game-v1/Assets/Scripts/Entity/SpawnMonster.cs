using System.Collections;
using UnityEngine;
public class SpawnMonster : MonoBehaviour
{
    private float xRange = 9.5f;
    private float yRange = 13f;

    public IEnumerator Spawn()
    {
        GameObject go;

        float delay = Managers.Game.creationCycle;

        while (Managers.Game.isSpawn)
        {
            int rand = Random.Range(0, Managers.Game.map.monsterType.Count);

            go = Managers.Game.objectPool.ActivateObject(typeof(Monster_Controller), Managers.Game.map.monsterType[rand].ToString());
            go.transform.position = GetRandomSpawnPosition();

            if (delay < 0.1f) { delay = 0.1f; }

            yield return new WaitForSeconds(delay);
        }
    }
    private Vector3 GetRandomSpawnPosition()
    {
        float x = Managers.Game.player.gameObject.transform.position.x;
        float y = Managers.Game.player.gameObject.transform.position.y;

        if (Random.Range(0, 2) == 1)
        {
            x += Random.Range(-xRange, xRange + 0.1f) >= 0 ? xRange : -xRange;
            y += Random.Range(-yRange, yRange + 0.1f);
        }
        else
        {
            y += Random.Range(-yRange, yRange + 0.1f) >= 0 ? -yRange : yRange;
            x += Random.Range(-xRange, xRange + 0.1f);
        }

        return new Vector3(x, y, 0);
    }
}
