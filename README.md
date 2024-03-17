# MapNinja for Rust streaming

This software is a simple OBS Websocket client that when G is pressed, the scene is automatically changed. This is intended to hide the map for streaming sessions


## Setup

- On your OBS, set websocket server on
- Create two scenes: **map** and **stream**
- Set the **map** scene as a copy of **stream** but change the input to be anything of your choice (but the video input)
- Connect to your OBS through your stream machine IP.
- Done!

## Behavior
When G is pressed down, the **map** scene should appear, instead of the **stream** scene

## TODO:
- Custom keybinding
- Custom scene names

