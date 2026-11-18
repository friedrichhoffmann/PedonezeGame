extends AnimatedSprite2D

func _ready():
	var frames = self.sprite_frames
	var animation = "idle"

	var frame_count = frames.get_frame_count(animation)

	for i in range(frame_count):
		var texture = frames.get_frame_texture(animation, i)
		var image = texture.get_image()

		image.save_png("user://frame_%02d.png" % i)
		print(OS.get_user_data_dir())

	print("Frames exportados!")
