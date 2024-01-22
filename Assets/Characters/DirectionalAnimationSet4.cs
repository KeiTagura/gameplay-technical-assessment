

using System;
using UnityEngine;
using Animancer;

namespace Kei
{
	[Serializable]
	public class DirectionalAnimationSet4
	{
		[SerializeField]
		private ClipTransition _Up;
		public ClipTransition Up => _Up;


		[SerializeField]
		private ClipTransition _Right;
		public ClipTransition Right => _Right;

		[SerializeField]
		private ClipTransition _Down;
		public ClipTransition Down => _Down;

		[SerializeField]
		private ClipTransition _Left;
		public ClipTransition Left => _Left;



		/************************************************************************************************************************/

		/// <summary>Returns the animation closest to the specified `direction`.</summary>
		public virtual ClipTransition GetClip(Vector2 direction)
		{
			var angle = Mathf.Atan2(direction.y, direction.x);
			var octant = Mathf.RoundToInt(8 * angle / (2 * Mathf.PI) + 8) % 8;
			switch (octant)
			{
				case 0: return _Right;
				case 2: return _Up;
				case 4: return _Left;
				case 6: return _Down;
				default: throw new ArgumentOutOfRangeException("Invalid octant");
			}
		}

		public enum Direction
		{
			/// <summary><see cref="Vector2.up"/>.</summary>
			Up,

			/// <summary><see cref="Vector2.right"/>.</summary>
			Right,

			/// <summary><see cref="Vector2.down"/>.</summary>
			Down,

			/// <summary><see cref="Vector2.left"/>.</summary>
			Left
		}

		protected virtual string GetDirectionName(int direction) => ((Direction)direction).ToString();

		/************************************************************************************************************************/

		/// <summary>Returns the animation associated with the specified `direction`.</summary>
		public ClipTransition GetClip(Direction direction)
		{
			switch (direction)
			{
				case Direction.Up: return _Up;
				case Direction.Right: return _Right;
				case Direction.Down: return _Down;
				case Direction.Left: return _Left;
				default: throw AnimancerUtilities.CreateUnsupportedArgumentException(direction);
			}
		}

		public virtual ClipTransition GetClip(int direction) => GetClip((Direction)direction);

		/************************************************************************************************************************/

		/// <summary>Sets the animation associated with the specified `direction`.</summary>
		public void SetClip(Direction direction, ClipTransition clip)
		{
			switch (direction)
			{
				case Direction.Up: _Up = clip; break;
				case Direction.Right: _Right = clip; break;
				case Direction.Down: _Down = clip; break;
				case Direction.Left: _Left = clip; break;
				default: throw AnimancerUtilities.CreateUnsupportedArgumentException(direction);
			}
		}

		public virtual void SetClip(int direction, ClipTransition clip) => SetClip((Direction)direction, clip);

		/************************************************************************************************************************/

		/// <summary>Returns a vector representing the specified `direction`.</summary>
		public static Vector2 DirectionToVector(Direction direction)
		{
			switch (direction)
			{
				case Direction.Up: return Vector2.up;
				case Direction.Right: return Vector2.right;
				case Direction.Down: return Vector2.down;
				case Direction.Left: return Vector2.left;
				default: throw AnimancerUtilities.CreateUnsupportedArgumentException(direction);
			}
		}

		public virtual Vector2 GetDirection(int direction) => DirectionToVector((Direction)direction);

		/************************************************************************************************************************/

		/// <summary>Returns the direction closest to the specified `vector`.</summary>
		public static Direction VectorToDirection(Vector2 vector)
		{
			var angle = Mathf.Atan2(vector.y, vector.x);
			var octant = Mathf.RoundToInt(8 * angle / (2 * Mathf.PI) + 8) % 8;
			switch (octant)
			{
				case 0: return Direction.Right;
				case 2: return Direction.Up;
				case 4: return Direction.Left;
				case 6: return Direction.Down;
				default: throw new ArgumentOutOfRangeException("Invalid octant");
			}
		}

		/************************************************************************************************************************/

		/// <summary>Returns a copy of the `vector` pointing in the closest direction this set type has an animation for.</summary>
		public static Vector2 SnapVectorToDirection(Vector2 vector)
		{
			var magnitude = vector.magnitude;
			var direction = VectorToDirection(vector);
			vector = DirectionToVector(direction) * magnitude;
			return vector;
		}

		public virtual Vector2 Snap(Vector2 vector) => SnapVectorToDirection(vector);

		public static class Diagonals
		{
			/************************************************************************************************************************/

			/// <summary>1 / (Square Root of 2).</summary>
			public const float OneOverSqrt2 = 0.70710678118f;

			/// <summary>A vector with a magnitude of 1 pointing up to the right.</summary>
			/// <remarks>The value is approximately (0.7, 0.7).</remarks>
			public static Vector2 UpRight => new Vector2(OneOverSqrt2, OneOverSqrt2);

			/// <summary>A vector with a magnitude of 1 pointing down to the right.</summary>
			/// <remarks>The value is approximately (0.7, -0.7).</remarks>
			public static Vector2 DownRight => new Vector2(OneOverSqrt2, -OneOverSqrt2);

			/// <summary>A vector with a magnitude of 1 pointing down to the left.</summary>
			/// <remarks>The value is approximately (-0.7, -0.7).</remarks>
			public static Vector2 DownLeft => new Vector2(-OneOverSqrt2, -OneOverSqrt2);

			/// <summary>A vector with a magnitude of 1 pointing up to the left.</summary>
			/// <remarks>The value is approximately (-0.707, 0.707).</remarks>
			public static Vector2 UpLeft => new Vector2(-OneOverSqrt2, OneOverSqrt2);

			/************************************************************************************************************************/
		}

		public ClipTransition[] GetClips()
		{
			ClipTransition[] clipArray = new ClipTransition[4] { _Up, _Right, _Down, _Left };
			return clipArray;
		}
	}
}