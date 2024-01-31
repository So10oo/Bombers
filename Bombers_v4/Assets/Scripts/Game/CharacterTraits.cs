using System;

public class CharacterTraits
{
    public event Action<CharacterTraits> OnCharacterTraitsChanged;

    public int FlameLength
    {
        get {  return _flameLength; }
        set { _flameLength = value; OnCharacterTraitsChanged?.Invoke(this); }
    }
    public float Speed 
    {
        get { return _speed; }
        set { _speed = value; OnCharacterTraitsChanged?.Invoke(this); }
    }
    public int BombQuantity
    {
        get {return _bombQuantity; }
        set { _bombQuantity = value; OnCharacterTraitsChanged?.Invoke(this); }
    }

    int _flameLength = 1;
    float _speed = 3.1f;
    int _bombQuantity = 1;


    public override string ToString()
    {
        var a = _flameLength.ToString();
        var b = _speed.ToString("N1");
        var c = _bombQuantity.ToString();

        return "Скорость: " + b + "\n" +
            "Радиус взрыва: " + a + "\n" +
            "Кол-во бомб: " + c;
    }

}
