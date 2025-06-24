using Verse;

namespace BloodTypes;

public class RecievedBloodTypeHediffWithComps : HediffWithComps
{
    private short _index;
    private BloodType bloodType;

    public override string LabelInBrackets => bloodType?.ToString();

    public override void PostTick()
    {
        _index %= 2579;
        if (_index == 0)
        {
            pawn.Clot();
        }

        _index++;
        base.PostTick();
    }

    public override void ExposeData()
    {
        Scribe_Deep.Look(ref bloodType, "BloodType");
        base.ExposeData();
    }
}