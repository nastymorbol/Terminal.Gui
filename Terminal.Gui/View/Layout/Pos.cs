using System.ComponentModel;

namespace Terminal.Gui;

/// <summary>
///     Indicates the side for <see cref="Pos"/> operations.
/// </summary>
public enum Side
{
    /// <summary>
    ///     The left (X) side of the view.
    /// </summary>
    Left = 0,

    /// <summary>
    ///     The top (Y) side of the view.
    /// </summary>
    Top = 1,

    /// <summary>
    ///     The right (X + Width) side of the view.
    /// </summary>
    Right = 2,

    /// <summary>
    ///     The bottom (Y + Height) side of the view.
    /// </summary>
    Bottom = 3
}

/// <summary>
///     Describes the position of a <see cref="View"/> which can be an absolute value, a percentage, centered, or
///     relative to the ending dimension. Integer values are implicitly convertible to an absolute <see cref="Pos"/>. These
///     objects are created using the static methods Percent, AnchorEnd, and Center. The <see cref="Pos"/> objects can be
///     combined with the addition and subtraction operators.
/// </summary>
/// <remarks>
///     <para>Use the <see cref="Pos"/> objects on the X or Y properties of a view to control the position.</para>
///     <para>
///         These can be used to set the absolute position, when merely assigning an integer value (via the implicit
///         integer to <see cref="Pos"/> conversion), and they can be combined to produce more useful layouts, like:
///         Pos.Center - 3, which would shift the position of the <see cref="View"/> 3 characters to the left after
///         centering for example.
///     </para>
///     <para>
///         Reference coordinates of another view by using the methods Left(View), Right(View), Bottom(View), Top(View).
///         The X(View) and Y(View) are aliases to Left(View) and Top(View) respectively.
///     </para>
///     <para>
///         <list type="table">
///             <listheader>
///                 <term>Pos Object</term> <description>Description</description>
///             </listheader>
///             <item>
///                 <term>
///                     <see cref="Pos.Align"/>
///                 </term>
///                 <description>
///                     Creates a <see cref="Pos"/> object that aligns a set of views.
///                 </description>
///             </item>
///             <item>
///                 <term>
///                     <see cref="Pos.Function(Func{int})"/>
///                 </term>
///                 <description>
///                     Creates a <see cref="Pos"/> object that computes the position by executing the provided
///                     function. The function will be called every time the position is needed.
///                 </description>
///             </item>
///             <item>
///                 <term>
///                     <see cref="Pos.Percent(float)"/>
///                 </term>
///                 <description>
///                     Creates a <see cref="Pos"/> object that is a percentage of the width or height of the
///                     SuperView.
///                 </description>
///             </item>
///             <item>
///                 <term>
///                     <see cref="Pos.AnchorEnd()"/>
///                 </term>
///                 <description>
///                     Creates a <see cref="Pos"/> object that is anchored to the end (right side or bottom) of
///                     the dimension, useful to flush the layout from the right or bottom.
///                 </description>
///             </item>
///             <item>
///                 <term>
///                     <see cref="Pos.Center"/>
///                 </term>
///                 <description>Creates a <see cref="Pos"/> object that can be used to center the <see cref="View"/>.</description>
///             </item>
///             <item>
///                 <term>
///                     <see cref="Absolute"/>
///                 </term>
///                 <description>
///                     Creates a <see cref="Pos"/> object that is an absolute position based on the specified
///                     integer value.
///                 </description>
///             </item>
///             <item>
///                 <term>
///                     <see cref="Pos.Left"/>
///                 </term>
///                 <description>
///                     Creates a <see cref="Pos"/> object that tracks the Left (X) position of the specified
///                     <see cref="View"/>.
///                 </description>
///             </item>
///             <item>
///                 <term>
///                     <see cref="Pos.X(View)"/>
///                 </term>
///                 <description>
///                     Creates a <see cref="Pos"/> object that tracks the Left (X) position of the specified
///                     <see cref="View"/>.
///                 </description>
///             </item>
///             <item>
///                 <term>
///                     <see cref="Pos.Top(View)"/>
///                 </term>
///                 <description>
///                     Creates a <see cref="Pos"/> object that tracks the Top (Y) position of the specified
///                     <see cref="View"/>.
///                 </description>
///             </item>
///             <item>
///                 <term>
///                     <see cref="Pos.Y(View)"/>
///                 </term>
///                 <description>
///                     Creates a <see cref="Pos"/> object that tracks the Top (Y) position of the specified
///                     <see cref="View"/>.
///                 </description>
///             </item>
///             <item>
///                 <term>
///                     <see cref="Pos.Right(View)"/>
///                 </term>
///                 <description>
///                     Creates a <see cref="Pos"/> object that tracks the Right (X+Width) coordinate of the
///                     specified <see cref="View"/>.
///                 </description>
///             </item>
///             <item>
///                 <term>
///                     <see cref="Pos.Bottom(View)"/>
///                 </term>
///                 <description>
///                     Creates a <see cref="Pos"/> object that tracks the Bottom (Y+Height) coordinate of the
///                     specified <see cref="View"/>
///                 </description>
///             </item>
///         </list>
///     </para>
/// </remarks>
public class Pos
{

    /// <summary>
    ///     Creates a <see cref="Pos"/> object that aligns a set of views according to the specified alignment setting.
    /// </summary>
    /// <param name="alignment"></param>
    /// <param name="groupId">
    ///     The optional, unique identifier for the set of views to align according to
    ///     <paramref name="alignment"/>.
    /// </param>
    /// <returns></returns>
    public static Pos Align (Alignment alignment, int groupId = 0) { return new PosAlign (alignment, groupId); }

    /// <summary>
    ///     Creates a <see cref="Pos"/> object that is anchored to the end (right side or
    ///     bottom) of the SuperView, minus the respective dimension of the View. This is equivalent to using
    ///     <see cref="Pos.AnchorEnd(int)"/>,
    ///     with an offset equivalent to the View's respective dimension.
    /// </summary>
    /// <returns>The <see cref="Pos"/> object anchored to the end (the bottom or the right side) minus the View's dimension.</returns>
    /// <example>
    ///     This sample shows how align a <see cref="Button"/> to the bottom-right the SuperView.
    ///     <code>
    /// anchorButton.X = Pos.AnchorEnd ();
    /// anchorButton.Y = Pos.AnchorEnd ();
    /// </code>
    /// </example>
    public static Pos AnchorEnd () { return new PosAnchorEnd (); }

    /// <summary>
    ///     Creates a <see cref="Pos"/> object that is anchored to the end (right side or bottom) of the SuperView,
    ///     useful to flush the layout from the right or bottom. See also <see cref="Pos.AnchorEnd()"/>, which uses the view
    ///     dimension to ensure the view is fully visible.
    /// </summary>
    /// <returns>The <see cref="Pos"/> object anchored to the end (the bottom or the right side).</returns>
    /// <param name="offset">The view will be shifted left or up by the amount specified.</param>
    /// <example>
    ///     This sample shows how align a 10 column wide <see cref="Button"/> to the bottom-right the SuperView.
    ///     <code>
    /// anchorButton.X = Pos.AnchorEnd (10);
    /// anchorButton.Y = 1
    /// </code>
    /// </example>
    public static Pos AnchorEnd (int offset)
    {
        if (offset < 0)
        {
            throw new ArgumentException (@"Must be positive", nameof (offset));
        }

        return new PosAnchorEnd (offset);
    }

    /// <summary>Creates a <see cref="Pos"/> object that is an absolute position based on the specified integer value.</summary>
    /// <returns>The Absolute <see cref="Pos"/>.</returns>
    /// <param name="position">The value to convert to the <see cref="Pos"/>.</param>
    public static Pos Absolute (int position) { return new PosAbsolute (position); }

    /// <summary>Creates a <see cref="Pos"/> object that can be used to center the <see cref="View"/>.</summary>
    /// <returns>The center Pos.</returns>
    /// <example>
    ///     This creates a <see cref="TextView"/> centered horizontally, is 50% of the way down, is 30% the height, and
    ///     is 80% the width of the <see cref="View"/> it added to.
    ///     <code>
    ///  var textView = new TextView () {
    ///     X = Pos.Center (),
    ///     Y = Pos.Percent (50),
    ///     Width = Dim.Percent (80),
    ///     Height = Dim.Percent (30),
    ///  };
    ///  </code>
    /// </example>
    public static Pos Center () { return new PosCenter (); }

    /// <summary>Determines whether the specified object is equal to the current object.</summary>
    /// <param name="other">The object to compare with the current object. </param>
    /// <returns>
    ///     <see langword="true"/> if the specified object  is equal to the current object; otherwise,
    ///     <see langword="false"/>.
    /// </returns>
    public override bool Equals (object other) { return other is Pos abs && abs == this; }

    /// <summary>
    ///     Creates a <see cref="Pos"/> object that computes the position by executing the provided function. The function
    ///     will be called every time the position is needed.
    /// </summary>
    /// <param name="function">The function to be executed.</param>
    /// <returns>The <see cref="Pos"/> returned from the function.</returns>
    public static Pos Function (Func<int> function) { return new PosFunc (function); }

    /// <summary>Serves as the default hash function. </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode () { return Anchor (0).GetHashCode (); }

    /// <summary>Adds a <see cref="Terminal.Gui.Pos"/> to a <see cref="Terminal.Gui.Pos"/>, yielding a new <see cref="Pos"/>.</summary>
    /// <param name="left">The first <see cref="Terminal.Gui.Pos"/> to add.</param>
    /// <param name="right">The second <see cref="Terminal.Gui.Pos"/> to add.</param>
    /// <returns>The <see cref="Pos"/> that is the sum of the values of <c>left</c> and <c>right</c>.</returns>
    public static Pos operator + (Pos left, Pos right)
    {
        if (left is PosAbsolute && right is PosAbsolute)
        {
            return new PosAbsolute (left.Anchor (0) + right.Anchor (0));
        }

        var newPos = new PosCombine (true, left, right);

        if (left is PosView view)
        {
            view.Target.SetNeedsLayout ();
        }

        return newPos;
    }

    /// <summary>Creates an Absolute <see cref="Pos"/> from the specified integer value.</summary>
    /// <returns>The Absolute <see cref="Pos"/>.</returns>
    /// <param name="n">The value to convert to the <see cref="Pos"/> .</param>
    public static implicit operator Pos (int n) { return new PosAbsolute (n); }

    /// <summary>
    ///     Subtracts a <see cref="Terminal.Gui.Pos"/> from a <see cref="Terminal.Gui.Pos"/>, yielding a new
    ///     <see cref="Pos"/>.
    /// </summary>
    /// <param name="left">The <see cref="Terminal.Gui.Pos"/> to subtract from (the minuend).</param>
    /// <param name="right">The <see cref="Terminal.Gui.Pos"/> to subtract (the subtrahend).</param>
    /// <returns>The <see cref="Pos"/> that is the <c>left</c> minus <c>right</c>.</returns>
    public static Pos operator - (Pos left, Pos right)
    {
        if (left is PosAbsolute && right is PosAbsolute)
        {
            return new PosAbsolute (left.Anchor (0) - right.Anchor (0));
        }

        var newPos = new PosCombine (false, left, right);

        if (left is PosView view)
        {
            view.Target.SetNeedsLayout ();
        }

        return newPos;
    }

    /// <summary>Creates a percentage <see cref="Pos"/> object</summary>
    /// <returns>The percent <see cref="Pos"/> object.</returns>
    /// <param name="percent">A value between 0 and 100 representing the percentage.</param>
    /// <example>
    ///     This creates a <see cref="TextField"/> centered horizontally, is 50% of the way down, is 30% the height, and
    ///     is 80% the width of the <see cref="View"/> it added to.
    ///     <code>
    ///  var textView = new TextField {
    ///      X = Pos.Center (),
    ///      Y = Pos.Percent (50),
    ///      Width = Dim.Percent (80),
    ///      Height = Dim.Percent (30),
    ///  };
    ///  </code>
    /// </example>
    public static Pos Percent (float percent)
    {
        if (percent is < 0 or > 100)
        {
            throw new ArgumentException ("Percent value must be between 0 and 100.");
        }

        return new PosPercent (percent / 100);
    }

    /// <summary>Creates a <see cref="Pos"/> object that tracks the Top (Y) position of the specified <see cref="View"/>.</summary>
    /// <returns>The <see cref="Pos"/> that depends on the other view.</returns>
    /// <param name="view">The <see cref="View"/>  that will be tracked.</param>
    public static Pos Top (View view) { return new PosView (view, Side.Top); }

    /// <summary>Creates a <see cref="Pos"/> object that tracks the Top (Y) position of the specified <see cref="View"/>.</summary>
    /// <returns>The <see cref="Pos"/> that depends on the other view.</returns>
    /// <param name="view">The <see cref="View"/>  that will be tracked.</param>
    public static Pos Y (View view) { return new PosView (view, Side.Top); }

    /// <summary>Creates a <see cref="Pos"/> object that tracks the Left (X) position of the specified <see cref="View"/>.</summary>
    /// <returns>The <see cref="Pos"/> that depends on the other view.</returns>
    /// <param name="view">The <see cref="View"/>  that will be tracked.</param>
    public static Pos Left (View view) { return new PosView (view, Side.Left); }

    /// <summary>Creates a <see cref="Pos"/> object that tracks the Left (X) position of the specified <see cref="View"/>.</summary>
    /// <returns>The <see cref="Pos"/> that depends on the other view.</returns>
    /// <param name="view">The <see cref="View"/>  that will be tracked.</param>
    public static Pos X (View view) { return new PosView (view, Side.Left); }

    /// <summary>
    ///     Creates a <see cref="Pos"/> object that tracks the Bottom (Y+Height) coordinate of the specified
    ///     <see cref="View"/>
    /// </summary>
    /// <returns>The <see cref="Pos"/> that depends on the other view.</returns>
    /// <param name="view">The <see cref="View"/>  that will be tracked.</param>
    public static Pos Bottom (View view) { return new PosView (view, Side.Bottom); }

    /// <summary>
    ///     Creates a <see cref="Pos"/> object that tracks the Right (X+Width) coordinate of the specified
    ///     <see cref="View"/>.
    /// </summary>
    /// <returns>The <see cref="Pos"/> that depends on the other view.</returns>
    /// <param name="view">The <see cref="View"/>  that will be tracked.</param>
    public static Pos Right (View view) { return new PosView (view, Side.Right); }

    /// <summary>
    ///     Gets a position that is anchored to a certain point in the layout. This method is typically used
    ///     internally by the layout system to determine where a View should be positioned.
    /// </summary>
    /// <param name="width">The width of the area where the View is being positioned (Superview.ContentSize).</param>
    /// <returns>
    ///     An integer representing the calculated position. The way this position is calculated depends on the specific
    ///     subclass of Pos that is used. For example, PosAbsolute returns a fixed position, PosAnchorEnd returns a
    ///     position that is anchored to the end of the layout, and so on.
    /// </returns>
    internal virtual int Anchor (int width) { return 0; }

    /// <summary>
    ///     Calculates and returns the position of a <see cref="View"/> object. It takes into account the dimension of the
    ///     superview and the dimension of the view itself.
    /// </summary>
    /// <param name="superviewDimension">
    ///     The dimension of the superview. This could be the width for x-coordinate calculation or the
    ///     height for y-coordinate calculation.
    /// </param>
    /// <param name="dim">The dimension of the View. It could be the current width or height.</param>
    /// <param name="us">The View that holds this Pos object.</param>
    /// <param name="dimension">Width or Height</param>
    /// <returns>
    ///     The calculated position of the View. The way this position is calculated depends on the specific subclass of Pos
    ///     that
    ///     is used.
    /// </returns>
    internal virtual int Calculate (int superviewDimension, Dim dim, View us, Dimension dimension) { return Anchor (superviewDimension); }

    /// <summary>
    ///     Diagnostics API to determine if this Pos object references other views.
    /// </summary>
    /// <returns></returns>
    internal virtual bool ReferencesOtherViews () { return false; }
}


/// <summary>
///     Enables alignment of a set of views.
/// </summary>
/// <remarks>
///     <para>
///         The Group ID is used to identify a set of views that should be alignment together. When only a single
///         set of views is aligned, setting the Group ID is not needed because it defaults to 0.
///     </para>
///     <para>
///         The first view added to the Superview with a given Group ID is used to determine the alignment of the group.
///         The alignment is applied to all views with the same Group ID.
///     </para>
/// </remarks>
public class PosAlign : Pos
{
    // BUGBUG: PosAlign should be internal like all other Pos classes. It is public because the PosAlign Scenario uses it. Refactor that Scenario.
    /// <summary>
    ///     The cached location. Used to store the calculated location to avoid recalculating it.
    /// </summary>
    private int? _location;

    /// <summary>
    ///     Gets the identifier of a set of views that should be aligned together. When only a single
    ///     set of views is aligned, setting the <see cref="_groupId"/> is not needed because it defaults to 0.
    /// </summary>
    private readonly int _groupId;

    /// <summary>
    ///     Gets the alignment settings.
    /// </summary>
    public Aligner Aligner { get; } = new ();

    /// <summary>
    ///     Aligns the views in <paramref name="views"/> that have the same group ID as <paramref name="groupId"/>.
    /// </summary>
    /// <param name="groupId"></param>
    /// <param name="views"></param>
    /// <param name="dimension"></param>
    /// <param name="size"></param>
    private static void AlignGroup (int groupId, IList<View> views, Dimension dimension, int size)
    {
        if (views is null)
        {
            return;
        }

        Aligner firstInGroup = null;
        List<int> dimensionsList = new ();

        List<View> viewsInGroup = views.Where (
                                               v =>
                                               {
                                                   if (dimension == Dimension.Width && v.X is PosAlign alignX)
                                                   {
                                                       return alignX._groupId == groupId;
                                                   }

                                                   if (dimension == Dimension.Height && v.Y is PosAlign alignY)
                                                   {
                                                       return alignY._groupId == groupId;
                                                   }

                                                   return false;
                                               })
                                       .ToList ();

        if (viewsInGroup.Count == 0)
        {
            return;
        }

        foreach (View view in viewsInGroup)
        {
            PosAlign posAlign = dimension == Dimension.Width ? view.X as PosAlign : view.Y as PosAlign;

            if (posAlign is { })
            {
                if (firstInGroup is null)
                {
                    firstInGroup = posAlign.Aligner;
                }

                dimensionsList.Add (dimension == Dimension.Width ? view.Frame.Width : view.Frame.Height);
            }
        }

        if (firstInGroup is null)
        {
            return;
        }

        firstInGroup.ContainerSize = size;
        int [] locations = firstInGroup.Align (dimensionsList.ToArray ());

        for (var index = 0; index < viewsInGroup.Count; index++)
        {
            View view = viewsInGroup [index];
            PosAlign align = dimension == Dimension.Width ? view.X as PosAlign : view.Y as PosAlign;

            if (align is { })
            {
                align._location = locations [index];
            }
        }
    }

    /// <summary>
    ///     Enables alignment of a set of views.
    /// </summary>
    /// <param name="alignment"></param>
    /// <param name="groupId">The unique identifier for the set of views to align according to <paramref name="alignment"/>.</param>
    public PosAlign (Alignment alignment, int groupId = 0)
    {
        Aligner.SpaceBetweenItems = true;
        Aligner.Alignment = alignment;
        _groupId = groupId;
        Aligner.PropertyChanged += Aligner_PropertyChanged;
    }

    private void Aligner_PropertyChanged (object sender, PropertyChangedEventArgs e) { _location = null; }

    /// <inheritdoc/>
    public override bool Equals (object other)
    {
        return other is PosAlign align && _groupId == align._groupId && _location == align._location && align.Aligner.Alignment == Aligner.Alignment;
    }

    /// <inheritdoc/>
    public override int GetHashCode () { return Aligner.GetHashCode () ^ _groupId.GetHashCode (); }

    /// <inheritdoc/>
    public override string ToString () { return $"Align(groupId={_groupId}, alignment={Aligner.Alignment})"; }

    internal override int Anchor (int width) { return _location ?? 0 - width; }

    internal override int Calculate (int superviewDimension, Dim dim, View us, Dimension dimension)
    {
        if (_location.HasValue && Aligner.ContainerSize == superviewDimension)
        {
            return _location.Value;
        }

        if (us?.SuperView is null)
        {
            return 0;
        }

        AlignGroup (_groupId, us.SuperView.Subviews, dimension, superviewDimension);

        if (_location.HasValue)
        {
            return _location.Value;
        }

        return 0;
    }
}

/// <summary>
///    Represents an absolute position in the layout. This is used to specify a fixed position in the layout.
/// </summary>
/// <remarks>
///     <para>
///     This is a low-level API that is typically used internally by the layout system. Use the various static
///     methods on the <see cref="Pos"/> class to create <see cref="Pos"/> objects instead.
///     </para>
/// </remarks>
/// <param name="position"></param>
public class PosAbsolute (int position) : Pos
{
    /// <summary>
    ///    The position of the <see cref="View"/> in the layout.
    /// </summary>
    public int Position { get; } = position;

    /// <inheritdoc />
    public override bool Equals (object other) { return other is PosAbsolute abs && abs.Position == Position; }

    /// <inheritdoc />
    public override int GetHashCode () { return Position.GetHashCode (); }

    /// <inheritdoc />
    public override string ToString () { return $"Absolute({Position})"; }

    internal override int Anchor (int width) { return Position; }
}

/// <summary>
///     Represents a position anchored to the end (right side or bottom).
/// </summary>
/// <remarks>
///     <para>
///     This is a low-level API that is typically used internally by the layout system. Use the various static
///     methods on the <see cref="Pos"/> class to create <see cref="Pos"/> objects instead.
///     </para>
/// </remarks>
public class PosAnchorEnd : Pos
{
    /// <summary>
    /// Gets the offset of the position from the right/bottom.
    /// </summary>
    public int Offset { get; }

    /// <summary>
    ///     Constructs a new position anchored to the end (right side or bottom) of the SuperView,
    ///     minus the respective dimension of the View. This is equivalent to using <see cref="PosAnchorEnd(int)"/>,
    ///     with an offset equivalent to the View's respective dimension.
    /// </summary>
    public PosAnchorEnd () { UseDimForOffset = true; }

    /// <summary>
    ///     Constructs a new position anchored to the end (right side or bottom) of the SuperView,
    /// </summary>
    /// <param name="offset"></param>
    public PosAnchorEnd (int offset) { Offset = offset; }

    /// <inheritdoc />
    public override bool Equals (object other) { return other is PosAnchorEnd anchorEnd && anchorEnd.Offset == Offset; }

    /// <inheritdoc />
    public override int GetHashCode () { return Offset.GetHashCode (); }

    /// <summary>
    ///     If true, the offset is the width of the view, if false, the offset is the offset value.
    /// </summary>
    public bool UseDimForOffset { get; }

    /// <inheritdoc />
    public override string ToString () { return UseDimForOffset ? "AnchorEnd()" : $"AnchorEnd({Offset})"; }

    internal override int Anchor (int width)
    {
        if (UseDimForOffset)
        {
            return width;
        }

        return width - Offset;
    }

    internal override int Calculate (int superviewDimension, Dim dim, View us, Dimension dimension)
    {
        int newLocation = Anchor (superviewDimension);

        if (UseDimForOffset)
        {
            newLocation -= dim.Anchor (superviewDimension);
        }

        return newLocation;
    }
}

/// <summary>
///     Represents a position that is centered.
/// </summary>
public class PosCenter : Pos
{
    /// <inheritdoc />
    public override string ToString () { return "Center"; }

    internal override int Anchor (int width) { return width / 2; }

    internal override int Calculate (int superviewDimension, Dim dim, View us, Dimension dimension)
    {
        int newDimension = Math.Max (dim.Calculate (0, superviewDimension, us, dimension), 0);

        return Anchor (superviewDimension - newDimension);
    }
}

/// <summary>
///    Represents a position that is a combination of two other positions.
/// </summary>
/// <remarks>
///     <para>
///     This is a low-level API that is typically used internally by the layout system. Use the various static
///     methods on the <see cref="Pos"/> class to create <see cref="Pos"/> objects instead.
///     </para>
/// </remarks>
/// <param name="add">Indicates whether the two positions are added or subtracted. If <see langword="true"/>, the positions are added, otherwise they are subtracted.</param>
/// <param name="left">The left position.</param>
/// <param name="right">The right position.</param>
public class PosCombine (bool add, Pos left, Pos right) : Pos
{
    /// <summary>
    /// Gets whether the two positions are added or subtracted. If <see langword="true"/>, the positions are added, otherwise they are subtracted.
    /// </summary>
    public bool Add { get; } = add;

    /// <summary>
    /// Gets the left position.
    /// </summary>
    public new Pos Left { get; } = left;

    /// <summary>
    /// Gets the right position.
    /// </summary>
    public new Pos Right { get; } = right;

    /// <inheritdoc />
    public override string ToString () { return $"Combine({Left}{(Add ? '+' : '-')}{Right})"; }

    internal override int Anchor (int width)
    {
        int la = Left.Anchor (width);
        int ra = Right.Anchor (width);

        if (Add)
        {
            return la + ra;
        }

        return la - ra;
    }

    internal override int Calculate (int superviewDimension, Dim dim, View us, Dimension dimension)
    {
        int newDimension = dim.Calculate (0, superviewDimension, us, dimension);
        int left = Left.Calculate (superviewDimension, dim, us, dimension);
        int right = Right.Calculate (superviewDimension, dim, us, dimension);

        if (Add)
        {
            return left + right;
        }

        return left - right;
    }

    internal override bool ReferencesOtherViews ()
    {
        if (Left.ReferencesOtherViews ())
        {
            return true;
        }

        if (Right.ReferencesOtherViews ())
        {
            return true;
        }

        return false;
    }
}

/// <summary>
///     Represents a position that is a percentage of the width or height of the SuperView.
/// </summary>
/// <remarks>
///     <para>
///     This is a low-level API that is typically used internally by the layout system. Use the various static
///     methods on the <see cref="Pos"/> class to create <see cref="Pos"/> objects instead.
///     </para>
/// </remarks>
/// <param name="percent"></param>
public class PosPercent (float percent) : Pos
{
    /// <summary>
    ///     Gets the factor that represents the percentage of the width or height of the SuperView.
    /// </summary>
    public new float Percent { get; } = percent;

    /// <inheritdoc />
    public override bool Equals (object other) { return other is PosPercent f && f.Percent == Percent; }

    /// <inheritdoc />
    public override int GetHashCode () { return Percent.GetHashCode (); }

    /// <inheritdoc />
    public override string ToString () { return $"Percent({Percent})"; }

    internal override int Anchor (int width) { return (int)(width * Percent); }
}

/// <summary>
///    Represents a position that is computed by executing a function that returns an integer position.
/// </summary>
/// <remarks>
///     <para>
///     This is a low-level API that is typically used internally by the layout system. Use the various static
///     methods on the <see cref="Pos"/> class to create <see cref="Pos"/> objects instead.
///     </para>
/// </remarks>
/// <param name="pos">The position.</param>
public class PosFunc (Func<int> pos) : Pos
{
    /// <summary>
    ///    Gets the function that computes the position.
    /// </summary>
    public Func<int> Func { get; } = pos;

    /// <inheritdoc />
    public override bool Equals (object other) { return other is PosFunc f && f.Func () == Func (); }

    /// <inheritdoc />
    public override int GetHashCode () { return Func.GetHashCode (); }

    /// <inheritdoc />
    public override string ToString () { return $"PosFunc({Func ()})"; }

    internal override int Anchor (int width) { return Func (); }
}

/// <summary>
///     Represents a position that is anchored to the side of another view.
/// </summary>
/// <remarks>
///     <para>
///     This is a low-level API that is typically used internally by the layout system. Use the various static
///     methods on the <see cref="Pos"/> class to create <see cref="Pos"/> objects instead.
///     </para>
/// </remarks>
/// <param name="view">The View the position is anchored to.</param>
/// <param name="side">The side of the View the position is anchored to.</param>
public class PosView (View view, Side side) : Pos
{
    /// <summary>
    /// Gets the View the position is anchored to.
    /// </summary>
    public View Target { get; } = view;

    /// <summary>
    /// Gets the side of the View the position is anchored to.
    /// </summary>
    public Side Side { get; } = side;

    /// <inheritdoc />
    public override bool Equals (object other) { return other is PosView abs && abs.Target == Target; }

    /// <inheritdoc />
    public override int GetHashCode () { return Target.GetHashCode (); }

    /// <inheritdoc />
    public override string ToString ()
    {
        string sideString = Side switch
        {
            Side.Left => "left",
            Side.Top => "top",
            Side.Right => "right",
            Side.Bottom => "bottom",
            _ => "unknown"
        };

        if (Target == null)
        {
            throw new NullReferenceException (nameof (Target));
        }

        return $"View(side={sideString},target={Target})";
    }

    internal override int Anchor (int width)
    {
        return Side switch
        {
            Side.Left => Target.Frame.X,
            Side.Top => Target.Frame.Y,
            Side.Right => Target.Frame.Right,
            Side.Bottom => Target.Frame.Bottom,
            _ => 0
        };
    }

    /// <summary>
    ///     Diagnostics API to determine if this Pos object references other views.
    /// </summary>
    /// <returns></returns>
    internal override bool ReferencesOtherViews () { return true; }
}
