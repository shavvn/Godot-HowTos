# Godot-HowTos

This is a doc of learning notes of Godot game engine (version 4.4 as the time of writing). A list of how-tos for a lot of atomic tasks.

### How to select a character by mouse click

Assuming the character/unit is an `CharacterBody2D` or any subclass of `CollisionObject2D` type:
- Create an input action for left mouse button (or right button) in InputMap within the project setting.
- The `Input/Pickable` property of the `CharacterBody2D` node needs to be set to on

    ![alt text](images/pickable.png)
- Connect the `input_event` signal to a handler function in the script, e.g. `_on_input_event`
- Within the `_on_input_event` function:

```
func _on_input_event(viewport: Node, event: InputEvent, shape_idx: int) -> void:
	if event is InputEventMouseButton:
		if event.is_action("mouse_left"):
            # do stuff here
			is_selected = true
```

### How to implement a in-game popup window

- Create a node of `Window` type
    - You could also use other `Control` types such as `Panel` but the `Window` type has more built-in implementations
- Draw the UI within the `Window` node
- Create InputMap key that triggers the popup, e.g. "tab"
- Attach script:

```
func _ready() -> void:
    # not shown by default
    hide()

func _input(event: InputEvent) -> void:
	if event.is_action_pressed("tab"):
		if visible:
			hide()
		else:
			show()
```

### How to move camera following mouse position (RTS style)

e.g. if you want to move the camera to the right when mouse is near the right edge of the viewport
- Add `Camera2D` child node to the "world" or map node
- Attach script to the `Camera2D` node
- Script:

```
# camera2d

# pan speed
@onready var pan_speed = 400
@onready var mouse_margin = 20  # move the camera if the mouse is near the edge

func _process(delta: float) -> void:
    var move_up_down: float = 0
	var move_left_right: float = 0
	var distance = delta * pan_speed
	var mouse_pos = get_viewport().get_mouse_position()
	
	if Input.is_action_pressed("ui_left") or mouse_pos.x < mouse_margin:
		move_left_right = -1
	if Input.is_action_pressed("ui_right") or mouse_pos.x > right_bounds - mouse_margin:
		move_left_right = 1
	if Input.is_action_pressed("ui_up") or mouse_pos.y < mouse_margin:
		move_up_down = -1
	if Input.is_action_pressed("ui_down") or mouse_pos.y > lower_bounds - mouse_margin:
		move_up_down = 1
	var move_vec = Vector2(move_left_right * distance, move_up_down * distance)
	if move_vec.length() > 0:
		position = position + move_vec
```