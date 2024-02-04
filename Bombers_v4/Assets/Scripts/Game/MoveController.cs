using UnityEngine;

namespace Assets.Scripts
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private protected LayerMask StopLayer;

        internal virtual Vector2 GetInput() => new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        internal virtual void SetMove(Vector2 direction, float speed)
        {
            if (Mathf.Abs(direction.y) < 0.1 && Mathf.Abs(direction.x) < 0.1)//для сломанных джостиков
                return;
            else
            {
                var s = speed * Time.deltaTime;
                var x = transform.position.x;
                var y = transform.position.y;
                float s1;
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    var roundY = Mathf.Round(y);
                    var deltaY = Mathf.Abs(y - roundY);
                    s1 = s - deltaY;
                    if (s1 > 0)
                    {
                        if (!Physics2D.OverlapPoint(new Vector2(Mathf.Round(x + direction.x * s1 + 0.5f * direction.x), roundY), StopLayer))
                            MovePosition(x + direction.x * s1, roundY);
                        else
                            MovePosition(Mathf.Round(x + direction.x * s1), roundY);
                    }
                    else
                        MovePosition(x, y + s * Mathf.Sign(roundY - y));

                }
                else 
                {
                    var roundX = Mathf.Round(x);
                    var deltaX = Mathf.Abs(x - roundX);
                    s1 = s - deltaX;
                    if (s1 > 0)
                    {
                        if (!Physics2D.OverlapPoint(new Vector2(roundX, Mathf.Round(y + direction.y * s1 + 0.5f * direction.y)), StopLayer))
                            MovePosition(roundX, y + direction.y * s1);
                        else
                            MovePosition(roundX, Mathf.Round(y + direction.y * s1));
                    }
                    else
                        MovePosition(x + s * Mathf.Sign(roundX - x), y);
                }
            }

            void MovePosition(float x, float y) => transform.position = new Vector2(x, y);

        }
    }
}
