using System;

public class PlayerHealth
{
    private float _hp;

    public float HP
    {
        get => _hp;

        set
        {
            _hp = value;

            if(_hp <= 0)
            {
                _hp = 0;

                Died?.Invoke();
            }

            HealthChanged?.Invoke(_hp);
        }
    }

    public event Action<float> HealthChanged;

    public event Action Died;

    public const float MAX_HP = 100;

    public PlayerHealth()
    {
        _hp = MAX_HP;
    }
}
