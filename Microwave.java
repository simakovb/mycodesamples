/**
 * Program Name: Microwave.java
 * Coder:		 Bohdan Simakov
 * Date: 	     July 1, 2020
 * Description:  Microwave represented as a GUI object.
 */

// import essential libraries.
import javax.swing.*;
import java.awt.*;
import java.awt.event.*;

public class Microwave extends JFrame
{
	private static final long serialVersionUID = 1L;//to turn off the warning in Eclipse
	
	// CLASS SCOPE VARIABLES HERE
	private String time = "";	
	private JTextArea foodTxtArea, timeTxtArea;
	private JButton button1, button2, button3, button4, button5, button6, button7, button8, button9,
							button0, buttonStart, buttonStop, buttonLevel, buttonDefrost, buttonTime;
	private JPanel foodArea;
	
	// 0-arg. constructor
	Microwave()
	{
		super("Microwave Oven");
		
		// boilerplate code
		this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		this.setLayout(new BorderLayout());
		this.setSize(800,400);
		this.setLocationRelativeTo(null);//Centers the frame on screen
		
		// build and add panels...
		
		foodArea = new JPanel();
		foodArea.setBackground(new Color(230, 230, 230));
		this.add(foodArea, BorderLayout.CENTER);
		
		JPanel timeAndButton = new JPanel();
		timeAndButton.setBackground(new Color(255, 255, 255));
		timeAndButton.setLayout(new BorderLayout());
		this.add(timeAndButton, BorderLayout.EAST);
		
		JPanel timeArea = new JPanel();
		timeArea.setBackground(new Color(255, 255, 255));
		timeAndButton.add(timeArea, BorderLayout.NORTH);
		
		JPanel buttonArea = new JPanel();
		buttonArea.setLayout(new GridLayout(0,3));
		buttonArea.setBackground(new Color(255, 255, 255));
		timeAndButton.add(buttonArea, BorderLayout.CENTER);
		
		foodTxtArea = new JTextArea("Oven Area -- Food Goes Here");
		foodTxtArea.setBackground(new Color(230, 230, 230));
		foodTxtArea.setEditable(false);
		foodArea.add(foodTxtArea);
		
		timeTxtArea = new JTextArea("Cooking time is displayed here...");
		timeTxtArea.setEditable(false);
		timeArea.add(timeTxtArea);
		
		button1 = new JButton("1");
		button2 = new JButton("2");
		button3 = new JButton("3");
		button4 = new JButton("4");
		button5 = new JButton("5");
		button6 = new JButton("6");
		button7 = new JButton("7");
		button8 = new JButton("8");
		button9 = new JButton("9");
		button0 = new JButton("0");
		buttonStart = new JButton("Start");
		buttonStop = new JButton("Stop");
		buttonLevel = new JButton("Level");
		buttonDefrost = new JButton("Defrost");
		buttonTime = new JButton("Time");
		
		buttonArea.add(buttonLevel);
		buttonArea.add(buttonDefrost);
		buttonArea.add(buttonTime);
		buttonArea.add(button1);
		buttonArea.add(button2);
		buttonArea.add(button3);
		buttonArea.add(button4);
		buttonArea.add(button5);
		buttonArea.add(button6);
		buttonArea.add(button7);
		buttonArea.add(button8);
		buttonArea.add(button9);
		buttonArea.add(button0);
		buttonArea.add(buttonStart);
		buttonArea.add(buttonStop);
		
		//register listeners for the buttons
		ButtonHandler handle = new ButtonHandler();
		
		button1.addActionListener(handle);
		button2.addActionListener(handle);
		button3.addActionListener(handle);
		button4.addActionListener(handle);
		button5.addActionListener(handle);
		button6.addActionListener(handle);
		button7.addActionListener(handle);
		button8.addActionListener(handle);
		button9.addActionListener(handle);
		button0.addActionListener(handle);
		
		OtherButtonHandler otherHandler = new OtherButtonHandler();
		
		buttonStart.addActionListener(otherHandler);
		buttonStop.addActionListener(otherHandler);
		buttonLevel.addActionListener(otherHandler);
		buttonDefrost.addActionListener(otherHandler);
		buttonTime.addActionListener(otherHandler);
		
		//last line
		this.setVisible(true);
	}//end constructor
	
	//INNER CLASSES GO HERE	
	private class ButtonHandler implements ActionListener
	{

		@Override
		public void actionPerformed(ActionEvent ev)
		{
			//limit the time String value to three digits, or four characters total including the semicolon
			if(time.length() < 4) 
			{
				String temp = timeTxtArea.getText();
				temp = temp + "\nPress Start";
				timeTxtArea.setText(temp);
			}
			else
			{
				// time string is at max length, so leave the method by calling return 
				// to prevent excess digits being appended to the time string. If user continues to click
				// on a numeric key, no new digits will be added to the time string.
				return;				
			}// end if-else
			
			// Now append the digits to the time string. Could also use a switch statement
			
			//After first digit is added to the string, we append the semicolon here
			if(time.length() == 1)
			{
				//append the semicolon after the first digit
				time += ":";
				timeTxtArea.setText(time);
			}
			
			// now append the digits
			if(ev.getActionCommand().equals("1")){
				time += "1";
				timeTxtArea.setText(time);
				
			}
			else if(ev.getActionCommand().equals("2")){
				time += "2";
				timeTxtArea.setText(time);
				
			}
			else if(ev.getActionCommand().equals("3")){
				time += "3";
				timeTxtArea.setText(time);
				
			}
			else if(ev.getActionCommand().equals("4")){
				time += "4";
				timeTxtArea.setText(time);
				
			}
			else if(ev.getActionCommand().equals("5")){
				time += "5";
				timeTxtArea.setText(time);
				
			}
			else if(ev.getActionCommand().equals("6")){
				time += "6";
				timeTxtArea.setText(time);
				
			}
			else if(ev.getActionCommand().equals("7")){
				time += "7";
				timeTxtArea.setText(time);
				
			}
			else if(ev.getActionCommand().equals("8")){
				time += "8";
				timeTxtArea.setText(time);
				
			}
			else if(ev.getActionCommand().equals("9")){
				time += "9";
				timeTxtArea.setText(time);
				
			}
			else if(ev.getActionCommand().equals("0")){
				time += "0";
				timeTxtArea.setText(time);
				
			}
			
		} //end of method	
		
	} //end inner of class
	
	
	private class OtherButtonHandler implements ActionListener
	{

		@Override
		public void actionPerformed(ActionEvent ev)
		{
			//see which button was pressed
			if(ev.getActionCommand().equals("Time"))
			{
				time = "";
				timeTxtArea.setText("Enter cooking Time: \n0:00");
			}
			else if(ev.getActionCommand().equals("Stop"))
			{
				time = "";
				timeTxtArea.setText("Cooking Time is displayed here....");
				foodArea.setBackground(new Color(230,230,230));
			}
			
			else if(ev.getActionCommand().equals("Defrost"))
			{
				time = "";
				timeTxtArea.setText("Food will begin defrosting at power level 2 now ....");
			}
			
			else if(ev.getActionCommand().equals("Level"))
			{
				time = "";
				timeTxtArea.setText("Enter a reduced power Level: \n0%");
			}
			
			else if(ev.getActionCommand().equals("Start"))
			{
				foodArea.setBackground(Color.YELLOW);
			}
			
		}// end of method
		
	}//end of inner class
	
	public static void main(String[] args)
	{
		//anonymous object
		new Microwave();
	}
	//end main
}
//end class