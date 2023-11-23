using System.Collections;
using UnityEngine;

namespace Menu
{
    public class CreditsManager : MonoBehaviour
    {
        [SerializeField] public float distanceToMove;
        [SerializeField] public float timeToMove;
    
        private bool _tileIsCurrentlyDisplayed;
        private GameObject _currentTile;

        public void DisplayCreditsTile(GameObject tileToDisplay)
        {
            if (tileToDisplay == _currentTile)
            {
                return;
            }
        
            var position = tileToDisplay.transform.position;
        
            if (_currentTile != null)
            {
                var oldTilePosition = _currentTile.transform.position;
            
                StartCoroutine(LiftUpOldTileAndLowerNewOne(
                    _currentTile, 
                    oldTilePosition, 
                    new Vector3(oldTilePosition.x, 
                        oldTilePosition.y + (distanceToMove / _currentTile.transform.localScale.y), 
                        oldTilePosition.z),
                    tileToDisplay, 
                    position, 
                    new Vector3(position.x, 
                        position.y - (distanceToMove / tileToDisplay.transform.localScale.y), 
                        position.z)));

                _currentTile = tileToDisplay;
            }
            else
            {
                StartCoroutine(MoveTile(tileToDisplay, position, 
                    new Vector3(position.x, 
                        position.y - (distanceToMove / tileToDisplay.transform.localScale.y), 
                        position.z)));

                _currentTile = tileToDisplay;
            }
        }

        public void ClearScreenAfterViewChange()
        {
            if (_currentTile == null)
            {
                return;
            }
        
            var position = _currentTile.transform.position;
        
            StartCoroutine(MoveTile(_currentTile, position, 
                new Vector3(position.x,
                    position.y + (distanceToMove / _currentTile.transform.localScale.y), 
                    position.z)));
        }

        private IEnumerator MoveTile(GameObject tile, Vector3 currentPosition, Vector3 desiredPosition)
        {
            float elapsedTime = 0;

            while (elapsedTime < timeToMove)
            {
                tile.transform.position = Vector3.Lerp(currentPosition, desiredPosition, elapsedTime / timeToMove);
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            tile.transform.position = desiredPosition;
        }

        private IEnumerator LiftUpOldTileAndLowerNewOne(
            GameObject oldTile, Vector3 currentPositionOld, Vector3 desiredPositionOld,
            GameObject newTile, Vector3 currentPositionNew, Vector3 desiredPositionNew)
        {
            yield return MoveTile(oldTile, currentPositionOld, desiredPositionOld);
            yield return MoveTile(newTile, currentPositionNew, desiredPositionNew);
        }
    }
}
