﻿using System;
using UnityEngine;

namespace Hermannia
{
	public class RegionSelector : MonoBehaviour
	{
		public static RegionSelector Instance { get; private set; }
		public Region CurrentRegion { get => _currentRegion; }
		public event Action RegionSelected;

		private static readonly int Region = Shader.PropertyToID("_Region");
		[SerializeField] private TextAsset regionData;
		private RegionColorHandler _regionColorHandler;
		private Region _currentRegion;
		private RegionHandler _regionHandler;
		private Material _material;

		private void Awake()
		{
			Instance = this;
			var spriteRenderer = GetComponent<SpriteRenderer>();
			_material = spriteRenderer.material;
			_regionColorHandler = new RegionColorHandler(spriteRenderer);
			_regionHandler = new RegionHandler(regionData.text);
			_material.SetColor(Region, Color.white);
		}

		private void OnMouseDown()
		{
			//Todo make this a manager-class
			//This logic probably should be delegated to another class if this becomes a manager-class
			var clickedColor = _regionColorHandler.GetSpritePixelColorUnderMousePointer();
			Region newRegion = null;
			if (clickedColor != Color.black)
			{
				newRegion = _regionHandler.GetRegionFromColor(clickedColor);
			}

			// Double selecting a region unselects it (optional feature)
			if (newRegion == _currentRegion || clickedColor == Color.black)
			{  
				newRegion = null;
				clickedColor = Color.white;
			}
			_material.SetColor(Region, clickedColor);
			_currentRegion = newRegion;
			
			RegionSelected?.Invoke();
		}
	}
}