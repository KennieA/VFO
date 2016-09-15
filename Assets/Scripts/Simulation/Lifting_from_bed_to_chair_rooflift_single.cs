﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lifting_from_bed_to_chair_rooflift_single : MonoBehaviour 
{	
	private void initializeExercise()
	{
        ToolBox tb = Util.ToggleResource<ToolBox>("ToolBox");
        if (tb)
        {
            tb.EmptyToolBox();
        }

        ToolGrid tg = Util.ToggleResource<ToolGrid>("ToolGrid");
        if (tg)
        {
            tg.SetToolCorrectness("Sengekontrol", true);
            tg.SetToolCorrectness("Liftkontrol", true);
            tg.SetToolCorrectness("Kørestol", true);
        }
        Util.ToggleResource<ToolGrid>("ToolGrid");

        HUD hud3 = Util.ToggleResource<HUD>("HUD_Helper");
        if (hud3)
        {
            hud3.Buttons[(int)Helper.Position.CENTERLEFT].Correct = true;
        }
        Util.ToggleResource<HUD>("HUD_Helper");

        GameObject go = GameObject.Find("Bed");
        if (go != null)
        {
            Util.ToggleSubElementRenderer(go, "bottom_left_slide");
            Util.ToggleSubElementRenderer(go, "bottom_right_slide");
            Util.ToggleSubElementRenderer(go, "up_left_slide");
            Util.ToggleSubElementRenderer(go, "up_right_slide");
            Util.ToggleSubElementRenderer(go, "antislide");
        }
	}

    private void defineExercise()
    {
        // Exercise States
        States.Instance.InitExerciseState(new string[] { "wheelchairPlaced" }, States.CheckCon.NotCritical, Text.Instance.GetString("sim_lift_bed_chair_roof_state_0"), 0.0f);
        States.Instance.InitExerciseState(new string[] { "hoover_lock" }, States.CheckCon.NotCritical, Text.Instance.GetString("sim_lift_bed_chair_roof_state_1"), 0.0f);
        States.Instance.InitExerciseState(new string[] { "WheelChairBack1" }, States.CheckCon.NotCritical, Text.Instance.GetString("sim_lift_bed_chair_roof_state_2"), 0.0f);
        States.Instance.InitExerciseState(new string[] { "RoofLiftDown1" }, States.CheckCon.NotCritical, Text.Instance.GetString("sim_lift_bed_chair_roof_state_3"), 0.0f);
        States.Instance.InitExerciseState(new string[] { "clickedSling1" }, States.CheckCon.NotCritical, Text.Instance.GetString("sim_lift_bed_chair_roof_state_4"), 0.0f);
        States.Instance.InitExerciseState(new string[] { "BedSittingUp" }, States.CheckCon.NotCritical, Text.Instance.GetString("sim_lift_bed_chair_roof_state_5"), 0.0f);
        States.Instance.InitExerciseState(new string[] { "Talk_0_0" }, States.CheckCon.NotCritical, Text.Instance.GetString("sim_lift_bed_chair_roof_state_60"), 0.0f);
        States.Instance.InitExerciseState(new string[] { "RoofLiftUp" }, States.CheckCon.NotCritical, Text.Instance.GetString("sim_lift_bed_chair_roof_state_6"), 0.0f);
        States.Instance.InitExerciseState(new string[] { "BedHeightDown" }, States.CheckCon.NotCritical, Text.Instance.GetString("sim_lift_bed_chair_roof_state_7"), 0.0f);
        States.Instance.InitExerciseState(new string[] { "Talk_0_1" }, States.CheckCon.NotCritical, Text.Instance.GetString("sim_lift_bed_chair_roof_state_8"), 0.0f);
        States.Instance.InitExerciseState(new string[] { "RoofLiftDown2" }, States.CheckCon.NotCritical, Text.Instance.GetString("sim_lift_bed_chair_roof_state_9"), 0.0f);
        States.Instance.InitExerciseState(new string[] { "clickedSling2" }, States.CheckCon.NotCritical, Text.Instance.GetString("sim_lift_bed_chair_roof_state_10"), 0.0f);
        States.Instance.InitExerciseState(new string[] { "WheelChairBack2" }, States.CheckCon.NotCritical, "", 4.0f);
        
        // Help text
        Help.Instance.AddHelpText(new string[] { "start" }, "sim_lift_bed_chair_roof_help_0");
        Help.Instance.AddHelpText(new string[] { "ToolboxDone" }, "sim_lift_bed_chair_roof_help_1");
		Help.Instance.AddHelpText(new string[] { "wheelchairPlaced" }, "sim_lift_bed_chair_roof_help_2");
		Help.Instance.AddHelpText(new string[] { "hoover_lock" }, "sim_lift_bed_chair_roof_help_3");
        Help.Instance.AddHelpText(new string[] { "WheelChairBack1" }, "sim_lift_bed_chair_roof_help_4");
		Help.Instance.AddHelpText(new string[] { "RoofLiftDown1" }, "sim_lift_bed_chair_roof_help_5");
		Help.Instance.AddHelpText(new string[] { "clickedSling1" }, "sim_lift_bed_chair_roof_help_6");
        Help.Instance.AddHelpText(new string[] { "BedSittingUp" }, "sim_lift_bed_chair_roof_help_70");
        Help.Instance.AddHelpText(new string[] { "Talk_0_0" }, "sim_lift_bed_chair_roof_help_7");
        Help.Instance.AddHelpText(new string[] { "RoofLiftUp" }, "sim_lift_bed_chair_roof_help_8");
		Help.Instance.AddHelpText(new string[] { "BedHeightDown" }, "sim_lift_bed_chair_roof_help_9");
		Help.Instance.AddHelpText(new string[] { "Talk_0_1" }, "sim_lift_bed_chair_roof_help_10");
		Help.Instance.AddHelpText(new string[] { "RoofLiftDown2" }, "sim_lift_bed_chair_roof_help_11");
		Help.Instance.AddHelpText(new string[] { "clickedSling2" }, "sim_lift_bed_chair_roof_help_12");
		
        // Dialog
        int pos1 = Talk.Instance.AddTalkDialog("hoover_head", "sim_lift_bed_chair_roof_talk_00");
        Talk.Instance.AddTalkDialog("Talk_0_0", pos1, "sim_lift_bed_chair_roof_talk_0");
        
        // Animations Female Helper
        AnimateFemale.Instance.SetIdle("1120_10");
        AnimateFemale.Instance.AddAnimation(new string[] { "clickedSling1" }, "AttachStraps", "1120_50", 0.0f, false, 10);
        AnimateFemale.Instance.AddAnimation(new string[] { "Talk_0_1" }, "MoveLift", "1110_90", 1.0f, false, 20, 1.0f, 0.0f);
        AnimateFemale.Instance.AddAnimation(new string[] { "RoofLiftDown2" }, "RoofLiftDownChair", "1110_100", 0.0f, false, 30, -1.0f, 2.0f);
        AnimateFemale.Instance.AddAnimation(new string[] { "clickedSling2" }, "DetachStraps", "1120_110", 0.0f, false, 40);

        // Animations Borger
        AnimateBorger.Instance.SetIdle("1110_10");
        AnimateBorger.Instance.AddAnimation(new string[] { "BedSittingUp" }, "BedSitting", "1110_60", 0.0f, false, 10, 1.0f, 0.0f);
        AnimateBorger.Instance.AddAnimation(new string[] { "RoofLiftUp" }, "LiftUp", "1110_70", 0.0f, false, 20, -1.0f, 2.0f);
        AnimateBorger.Instance.AddAnimation(new string[] { "BedHeightDown" }, "LowerBed", "1110_80", 0.0f, false, 30);
        AnimateBorger.Instance.AddAnimation(new string[] { "Talk_0_1" }, "MoveLift", "1110_90", 4.0f, false, 40, 1.0f, 0.0f);
        AnimateBorger.Instance.AddAnimation(new string[] { "RoofLiftDown2" }, "RoofLiftDownChair", "1110_100", 0.2f, false, 50, -1.0f, 2.0f);
        AnimateBorger.Instance.AddAnimation(new string[] { "WheelChairBack2" }, "MoveChairForward", "1110_120", 0.0f, false, 60);

        // AnimateSling
        AnimateSling2.Instance.SetIdle("1110_10");
        AnimateSling2.Instance.AddAnimation(new string[] { "clickedSling1" }, "AttachStraps", "1120_50", 0.0f, false, 10);
        AnimateSling2.Instance.AddAnimation(new string[] { "BedSittingUp" }, "BedSitting", "1110_60", 0.0f, false, 20);
        AnimateSling2.Instance.AddAnimation(new string[] { "RoofLiftUp" }, "RoofLiftUp", "1110_70", 0.0f, false, 30);
        AnimateSling2.Instance.AddAnimation(new string[] { "Talk_0_1" }, "MoveLift", "1110_90", 4.0f, false, 40, 1.0f, 0.0f);
        AnimateSling2.Instance.AddAnimation(new string[] { "RoofLiftDown2" }, "LowerLift", "1110_100", 0.0f, false, 50);
        AnimateSling2.Instance.AddAnimation(new string[] { "clickedSling2" }, "DetachStrops", "1120_110", 0.0f, false, 60);
        AnimateSling2.Instance.AddAnimation(new string[] { "WheelChairBack2" }, "ChairForward", "1110_120", 0.0f, false, 70);

        // Animate Bed
        AnimateBed3.Instance.SetIdle("base");
        AnimateBed3.Instance.AddAnimation(new string[] { "start" }, "rail_down_left", 1.0f, 0.0f);
        AnimateBed3.Instance.AddAnimation(new string[] { "start" }, "rail_down", 1.0f, 0.0f);
        AnimateBed3.Instance.AddAnimation(new string[] { "BedSittingUp" }, "530_20", 1.0f, 4.0f);
        AnimateBed3.Instance.AddAnimation(new string[] { "BedHeightDown" }, "530_30", 0.9f, 3.0f);
			
        // Animate Floor Lift
        AnimateRoofLift.Instance.SetIdle("1110_10");
        AnimateRoofLift.Instance.AddAnimation(new string[] { "RoofLiftDown1" }, "RoofliftDownBed", "1110_40", 0.0f, false, 10);
        AnimateRoofLift.Instance.AddAnimation(new string[] { "RoofLiftUp" }, "RoofliftUp", "1110_60", 0.0f, false, 20);
        AnimateRoofLift.Instance.AddAnimation(new string[] { "Talk_0_1" }, "RoofLiftBack", "1110_90", 4.0f, false, 30, 1.0f, 0.0f);
        AnimateRoofLift.Instance.AddAnimation(new string[] { "RoofLiftDown2" }, "RoofLiftDownChair", "1110_100", 0.0f, false, 40);

        // Animate Wheelchair
        AnimateWheelChair3.Instance.AddAnimation(new string[] { "wheelchairPlaced" }, "wheelchairPlaced", "1110_10", 0.0f, false, 10);
        AnimateWheelChair3.Instance.AddAnimation(new string[] { "hoover_lock" }, "Lock", "1110_20", 0.0f, false, 20);
        AnimateWheelChair3.Instance.AddAnimation(new string[] { "WheelChairBack1" }, "MoveBack", "1110_30", 0.0f, false, 30);
        AnimateWheelChair3.Instance.AddAnimation(new string[] { "WheelChairBack2" }, "MoveForward", "1110_120", 0.0f, false, 40);
        AnimateWheelChair3.Instance.AddResetState(new string[] { "wheelchairPlaced" });


        MoveAndFadeCamera.Instance.AddCameraFade(new string[] { "start" }, 0, 0.0f, 0.0f);
        MoveAndFadeCamera.Instance.AddCameraFade(new string[] { "RoofLiftDown1" }, 1, 0.3f, 1.0f);
        MoveAndFadeCamera.Instance.AddCameraFade(new string[] { "Talk_0_1" }, 2, 0.3f, 1.0f);
    }

    public void SimCallback(string t)
    {
        if (States.Instance.GetStateValueB("showingErrorMessage"))
            return;

        if (t == "clickedSling")
        {
            if (!States.Instance.GetExersiciseValue("clickedSling1"))
                t = "clickedSling1";
            else
                t = "clickedSling2";
        }

        if (t == "RoofLiftDown")
        {
            if (!States.Instance.GetExersiciseValue("RoofLiftDown1"))
                t = "RoofLiftDown1";
            else
                t = "RoofLiftDown2";
        }

        if (t == "hoover_wheelchair_handle")
        {
            if (!States.Instance.GetExersiciseValue("WheelChairBack1"))
                t = "WheelChairBack1";
            else
                t = "WheelChairBack2";
        }

        Debug.Log(t);


        if (t != _currentState && !States.Instance.GetExersiciseValue(t) && !States.Instance.HasFinished())
        {
            _currentState = t;
            int rv = States.Instance.UpdateState(t, help);
            Debug.Log("Rv: " + rv.ToString());
            if (rv != -1)
            {
                if (!States.Instance.GetExerciseCritical(rv))
                {
                    States.Instance.PushState("showingErrorMessage");
                    Util.OkMessageBox(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 100, 300, 200), "\n\n" + States.Instance.GetExerciseError(), OkClicked);
                    Results.Instance.SubtractStar();
                    StarFade.Instance.ShowStar(new Rect((Screen.width / 2 - 138), Screen.height / 2 - 90, 138, 90), false);
                }
                else
                {
                    Results.Instance.ShowResults(true, Text.Instance.GetString("results_failed") + "\n\n" + States.Instance.GetExerciseError(), 0.0f);
                    if (States.Instance.GetExerciseVideo() != -1) Video.Instance.PlayMovie(States.Instance.GetExerciseVideo());
                }
            }
            else
            {
                if (help)
                {
                    Help.Instance.UpdateHelp(t);
                }

                if (t == "clickedSling1" || t == "clickedSling2")
                {
                    GameObject go = GameObject.Find("sail");
                    if (go)
                        go.layer = LayerMask.NameToLayer("Default");
                }
                else if (t == "RoofLiftDown2")
                {
                    GameObject go = GameObject.Find("sail");
                    if (go)
                        go.layer = LayerMask.NameToLayer("mouseOver");
                }

                Talk.Instance.UpdateTalk(t);
                AnimateFemale.Instance.UpdateAnimation(t);
                AnimateBorger.Instance.UpdateAnimation(t);
                AnimateBed3.Instance.UpdateAnimation(t);
                AnimateSling2.Instance.UpdateAnimation(t);
                AnimateRoofLift.Instance.UpdateAnimation(t);
				MoveAndFadeCamera.Instance.UpdateCameraFade(t);
                AnimateWheelChair3.Instance.UpdateAnimation(t);

                if (States.Instance.HasFinished())
                {
                    string s = help ? Text.Instance.GetString("results_passed_help") : Text.Instance.GetString("results_passed_test");

                    string rms = States.Instance.GetComments();
                    s += rms.Length > 1 ? "\n\n" + Text.Instance.GetString("results_comment") + " " + rms : "\n";

                    Results.Instance.ShowResults(false, help, s, States.Instance.GetExerciseDelay(States.Instance.CurrentState()));
                }
            }
        }
    }

    public void OkClicked(Message message, bool value)
    {
        States.Instance.PushState("showingErrorMessage", "no");
        StarFade.Instance.HideStar();
    }

    // Temp test of states
    public string _currentState = "";
    public bool help = false;

 	// Use this for initialization
	void Start () 
	{
        // Clear old states
		States.Instance.ClearStates();
		
		// Set callback name to this gameobject
		States.Instance.PushState("actionCallbackGameObjectName", gameObject.name);
		
        // If run in editor or not
		if(SceneLoader.Instance.CurrentScene != -1) {
			help = Global.Instance.RunSimulationWithHelp;
		}
        else {
            States.Instance.PushState("DEBUG");
            GameObject.Instantiate((GameObject)Resources.Load("BottomBar"));
            GameObject.Instantiate((GameObject)Resources.Load("TopBar"));
        }
		
        // Initialize and define simulation
		initializeExercise();
        defineExercise();
		
		if(help) {
			Help.Instance.ShowHelpText();
		}
			
		// trick for not displaying the toolbox
		States.Instance.PushState("ToolboxDone", "yes");

        // Start the simulation
        SimCallback("start");
	}
	
	// Update is called once per frame
	void Update () 
	{
        if(Input.GetKeyDown(KeyCode.D))
        {
            States.Instance.DebugState();
        }
	}
}