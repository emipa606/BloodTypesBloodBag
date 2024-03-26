using Verse;

namespace BloodTypes;

public class BloodType : IExposable
{
    public BloodTypes Primary;
    public Rh RhPrimary;
    public Rh RhSecondary;
    public BloodTypes Secondary;

    public void ExposeData()
    {
        Scribe_Values.Look(ref Primary, "ABO1");
        Scribe_Values.Look(ref Secondary, "ABO2");
        Scribe_Values.Look(ref RhPrimary, "RH1");
        Scribe_Values.Look(ref RhSecondary, "RH2");
    }


    public Rh RhExpressed()
    {
        if (RhPrimary == Rh.Pos || RhSecondary == Rh.Pos)
        {
            return Rh.Pos;
        }

        return Rh.Neg;
    }

    public ExpressedBloodTypes ExpressedBloodType()
    {
        var expressed = (ExpressedBloodTypes)Primary;
        if (Primary == Secondary)
        {
            return expressed;
        }

        if (Primary == BloodTypes.O)
        {
            return (ExpressedBloodTypes)Secondary;
        }

        if (Secondary == BloodTypes.O)
        {
            return (ExpressedBloodTypes)Primary;
        }

        return ExpressedBloodTypes.AB;
    }

    public bool CanGetBlood(BloodType other)
    {
        if (other == null)
        {
            return true;
        }

        var expressed = ExpressedBloodType();
        var otherExpressed = other.ExpressedBloodType();
        if (RhExpressed() == Rh.Neg)
        {
            if (other.RhExpressed() == Rh.Pos)
            {
                return false;
            }
        }

        if (otherExpressed == ExpressedBloodTypes.O)
        {
            return true;
        }

        if (expressed == ExpressedBloodTypes.AB)
        {
            return true;
        }

        return expressed == otherExpressed;
    }

    public override string ToString()
    {
        return ExpressedBloodType() + (RhExpressed() == Rh.Pos ? "+" : "-");
    }

    public static BloodType Random()
    {
        return new BloodType
        {
            Primary = (BloodTypes)(Rand.Value * 3),
            Secondary = (BloodTypes)(Rand.Value * 3),
            RhPrimary = (Rh)(Rand.Value * 2),
            RhSecondary = (Rh)(Rand.Value * 2)
        };
    }

    public BloodType Child(BloodType other = null)
    {
        if (other == null)
        {
            other = Random();
        }

        return new BloodType
        {
            Primary = new[] { other.Primary, Primary }.RandomElement(),
            Secondary = new[] { other.Secondary, Primary }.RandomElement(),
            RhPrimary = new[] { other.RhPrimary, RhPrimary }.RandomElement(),
            RhSecondary = new[] { other.RhSecondary, RhSecondary }.RandomElement()
        };
    }

    public bool Equals(BloodType other)
    {
        return ExpressedBloodType().Equals(other.ExpressedBloodType()) && RhExpressed().Equals(other.RhExpressed());
    }

    public override bool Equals(object obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        return obj.GetType() == GetType() && Equals((BloodType)obj);
    }

    public override int GetHashCode()
    {
        var hashCode = 1273248884;
        hashCode = (hashCode * -1521134295) + Primary.GetHashCode();
        hashCode = (hashCode * -1521134295) + Secondary.GetHashCode();
        hashCode = (hashCode * -1521134295) + RhPrimary.GetHashCode();
        hashCode = (hashCode * -1521134295) + RhSecondary.GetHashCode();
        return hashCode;
    }
}