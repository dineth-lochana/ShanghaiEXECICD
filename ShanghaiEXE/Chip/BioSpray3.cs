﻿using NSAttack;
using NSBattle;
using NSBattle.Character;
using NSShanghaiEXE.InputOutput;
using NSShanghaiEXE.InputOutput.Audio;
using NSShanghaiEXE.InputOutput.Rendering;
using Common.Vectors;
using System.Drawing;

namespace NSChip
{
    internal class BioSpray3 : ChipBase
  {
    private const int shotend = 10;

    public BioSpray3(IAudioEngine s)
      : base(s)
    {
      this.rockOnPoint = new Point(-2, 0);
      this.number = 135;
      this.name = NSGame.ShanghaiEXE.Translate("Chip.BioSpray3Name");
      this.element = ChipBase.ELEMENT.poison;
      this.power = 240;
      this.regsize = 48;
      this.reality = 3;
      this.subpower = 2;
      this._break = false;
      this.powerprint = true;
      this.code[0] = ChipFolder.CODE.C;
      this.code[1] = ChipFolder.CODE.Q;
      this.code[2] = ChipFolder.CODE.W;
      this.code[3] = ChipFolder.CODE.Z;
      var information = NSGame.ShanghaiEXE.Translate("Chip.BioSpray3Desc");
      this.information[0] = information[0];
      this.information[1] = information[1];
      this.information[2] = information[2];
      this.Init();
    }

    public override void Action(CharacterBase character, SceneBattle battle)
    {
      if (character.animationpoint.X != 5 || character.waittime == 0)
      {
        this.sound.PlaySE(SoundEffect.switchon);
        character.animationpoint = new Point(5, 0);
      }
      bool gas = false;
      if (character.waittime % 2 == 0)
      {
        gas = true;
        this.sound.PlaySE(SoundEffect.lance);
      }
      battle.attacks.Add(new PoisonGas(this.sound, battle, character.position.X + 2 * this.UnionRebirth(character.union), character.position.Y - 1, character.union, this.subpower, gas, this.element));
      battle.attacks.Add(new PoisonGas(this.sound, battle, character.position.X + 2 * this.UnionRebirth(character.union), character.position.Y, character.union, this.subpower, gas, this.element));
      battle.attacks.Add(new PoisonGas(this.sound, battle, character.position.X + 2 * this.UnionRebirth(character.union), character.position.Y + 1, character.union, this.subpower, gas, this.element));
      ++this.frame;
      if (this.frame < this.power / this.subpower && (!Input.IsUp(Button._A) || !(character is Player)))
        return;
      this.frame = 0;
      base.Action(character, battle);
    }

    public override void GraphicsRender(
      IRenderer dg,
      Vector2 p,
      int c,
      bool printgraphics,
      bool printstatus)
    {
      if (printgraphics)
      {
        this._rect = new Rectangle(280, 0, 56, 48);
        dg.DrawImage(dg, "chipgraphic5", this._rect, true, p, Color.White);
      }
      base.GraphicsRender(dg, p, c, printgraphics, printstatus);
    }

    public override void IconRender(
      IRenderer dg,
      Vector2 p,
      bool select,
      bool custom,
      int c,
      bool noicon)
    {
      if (!noicon)
      {
        this._rect = this.IconRect(select);
        dg.DrawImage(dg, "chipicon", this._rect, true, p, Color.White);
      }
      base.IconRender(dg, p, select, custom, c, noicon);
    }

    public override void Render(IRenderer dg, CharacterBase character)
    {
      if (character.waittime < 0)
        return;
      this._rect = new Rectangle(8 * character.Wide, character.Height, character.Wide, character.Height);
      this._position = new Vector2(character.positionDirect.X + Shake.X, character.positionDirect.Y + Shake.Y);
      if (character.waittime % 2 == 1)
        this._rect.X += character.Wide;
      dg.DrawImage(dg, "weapons", this._rect, false, this._position, character.union == Panel.COLOR.blue, Color.White);
    }
  }
}

