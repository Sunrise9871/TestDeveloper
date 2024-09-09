using UnityEngine;
using JetBrains.Annotations;

namespace CookingPrototype.Kitchen {
	[RequireComponent(typeof(FoodPlace))]
	public sealed class FoodTrasher : MonoBehaviour {
		private const float DOUBLE_TAP_TIME = 0.5f;

		FoodPlace _place = null;
		float     _timer = 0f;

		void Start() {
			_place = GetComponent<FoodPlace>();
			_timer = Time.realtimeSinceStartup;
		}

		/// <summary>
		/// Освобождает место по двойному тапу если еда на этом месте сгоревшая.
		/// </summary>
		[UsedImplicitly]
		public void TryTrashFood() {
			if ( Time.realtimeSinceStartup - _timer > DOUBLE_TAP_TIME ) {
				_timer = Time.realtimeSinceStartup;
				return;
			}
			_timer = 0f;

			if ( _place.IsFree ) {
				return;
			}

			var food = _place.CurFood;
			if ( food.CurStatus == Food.FoodStatus.Overcooked ) {
				_place.FreePlace();
			}
		}
	}
}