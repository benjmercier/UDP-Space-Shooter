using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.ExtensionMethods
{
    public static class ComponentExtensions
    {
        public static bool TryGetComponentInParents<T>(this Component thisComponent, out T component)
        {
            if (thisComponent.TryGetComponent(out component))
            {
                return true;
            }

            if (thisComponent.transform.parent != null)
            {
                if (thisComponent.transform.parent.TryGetComponentInParents(out component))
                {
                    return true;
                }
            }

            return false;
        }

        //public static bool TryGetComponentInParents<T>(this Component thisComponent, out T component) where T : class?
        //{
        //    component = null;
        //    Component currentComponent = thisComponent;

        //    while (component == null)
        //    {
        //        if (currentComponent.TryGetComponent(out T componentCheck))
        //        {
        //            component = componentCheck;

        //            break;
        //        }

        //        if (currentComponent.transform.parent != null)
        //        {
        //            currentComponent = currentComponent.transform.parent;
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }

        //    return component != null;
        //}
    }
}
