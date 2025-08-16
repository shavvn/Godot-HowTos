using Godot;
using System;

public partial class HexTileMapLayer : TileMapLayer
{
	private ShaderMaterial _shaderMaterial;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this._shaderMaterial = this.Material as ShaderMaterial;  // cast
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 localMousePosition = ToLocal(GetGlobalMousePosition());
		Vector2I tileCoords = LocalToMap(localMousePosition);
		Vector2 centerCoords = MapToLocal(tileCoords);
		Vector2 activeCenter = ToGlobal(centerCoords);

		if (GetCellSourceId(tileCoords) != -1)
		{
			_shaderMaterial?.SetShaderParameter("active_hex_center", activeCenter);
		}
	}
}
