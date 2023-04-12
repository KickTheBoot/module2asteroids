using UnityEngine;
public static class EdgeWrapSystem
{

    //Imitating the screen wrap effect
    public static Vector3 screenWrapPosition(Vector3 position)
    {
        byte areainfo = PlayaArea.instance.ObjectWithinBoundsInfo(position);
        //Debug.Log(areainfo);
        Vector3 screenwrapposition = position;
        if ((areainfo & 0b0001) == 0b0001) screenwrapposition.x = PlayaArea.instance.bounds.width * -0.5f;
        if ((areainfo & 0b0010) == 0b0010) screenwrapposition.x = PlayaArea.instance.bounds.width * 0.5f;
        if ((areainfo & 0b0100) == 0b0100) screenwrapposition.y = PlayaArea.instance.bounds.height * -0.5f;
        if ((areainfo & 0b1000) == 0b1000) screenwrapposition.y = PlayaArea.instance.bounds.height * 0.5f;

        return screenwrapposition;
    }
}